using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Responses;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ip2country_desktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            DrawingControl.SetDoubleBuffered(this);
            DrawingControl.SetDoubleBuffered(dg);
        }

        internal enum UniqueIPstate
        {
            notActive,
            IP,
            IPrange
        }

        private SortableBindingList<ApacheAccessModel> x;
        private UniqueIPstate isUniqueIPactive = UniqueIPstate.notActive;

        private void btnPaste_Click(object sender, EventArgs e)
        {
            LoadData2Grid(General.GetFromClipboard());
        }

        private void LoadData2Grid(string txtLines)
        {
            DrawingControl.SuspendDrawing(dg);

            dg.DataSource = null;
            dg.Rows.Clear();
            dg.Columns.Clear();
            btnRemoveFilter.Enabled = false; isUniqueIPactive = UniqueIPstate.notActive;

            string pattern = @"\b(?:\d{1,3}\.){3}\d{1,3}\b";
            string patternLine = @"^(?<ip>.*?) - - (?<dt>.*?) ""(?<request>.*?)"" (?<status>.*?) (?<size>.*?)$";

            string s2;

            x = new SortableBindingList<ApacheAccessModel>();
            Regex regex = new Regex(patternLine, RegexOptions.Compiled);
            Match match;

            ApacheAccessModel y;
            string[] country;
            foreach (string s in txtLines.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None))
            {
                s2 = s.Trim();

                y = new ApacheAccessModel();

                if (s2.Length > 20)
                {

                    match = regex.Match(s2);

                    if (match.Success)
                    {
                        y.ip = match.Groups["ip"].Value;
                        y.date = match.Groups["dt"].Value;
                        y.request = match.Groups["request"].Value;
                        y.status = match.Groups["status"].Value;
                        y.size = match.Groups["size"].Value;
                        country = GetCountryByIP(y.ip);
                        y.country = country[0];
                        y.iprange = country[1];
                    }
                }
                else
                {
                    match = new Regex(pattern).Match(s2);
                    if (match.Success)
                    {
                        y.ip = match.Value;
                        country = GetCountryByIP(y.ip);
                        y.country = country[0];
                        y.iprange = country[1];
                    }
                    else
                        y.ip = null;
                }

                x.Add(y);
            }

            dg.DataSource = x;
            DrawingControl.ResumeDrawing(dg);

            this.Text = Application.ProductName + " - rows : " + dg.RowCount;
        }

        private static DatabaseReader dbIP = null;
        private static string[] GetCountryByIP(string ip)
        {
            try
            {
                if (dbIP == null)
                    dbIP = new DatabaseReader(@"GeoLite2-Country.mmdb");

                CountryResponse data = dbIP.Country(ip);

                if (data.Country.IsoCode == null)
                {
                    if (data.Continent != null && data.Traits.Network.NetworkAddress != null && data.Traits.Network.PrefixLength != null)
                        return new string[] { data.Continent.Name, data.Traits.Network.NetworkAddress + "/" + data.Traits.Network.PrefixLength.ToString() };

                    if (data.Continent != null)
                        return new string[] { data.Continent.Name, "" };

                    return new string[] { "N/A", "" };
                }
                else
                    return new string[] { data.Country.Name, data.Traits.Network.NetworkAddress + "/" + data.Traits.Network.PrefixLength.ToString() };

            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException || ex is System.IO.FileNotFoundException)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }

                return new string[] { "N/A", "" };
            }
        }

        private void btnUniqueIP_Click(object sender, EventArgs e)
        {
            bool uniqueIPrange = Control.ModifierKeys == Keys.Control;

            isUniqueIPactive = UniqueIPstate.notActive; if (x == null) return;

            dg.DataSource = null;
            dg.Rows.Clear();
            dg.Columns.Clear();

            if (uniqueIPrange)
            {
                isUniqueIPactive = UniqueIPstate.IPrange;
                dg.DataSource = new SortableBindingList<ApacheAccessModel>(x.AsEnumerable().GroupBy(log => log.iprange)
                .Select(group => group.First())
                .OrderBy(log => log.country)
                .ToList());
            }
            else
            {
                isUniqueIPactive = UniqueIPstate.IP;
                dg.DataSource = new SortableBindingList<ApacheAccessModel>(x.AsEnumerable().GroupBy(log => log.ip)
                .Select(group => group.First())
                .OrderBy(log => log.country)
                .ToList());
            }

            this.Text = Application.ProductName + " - rows : " + dg.RowCount;
            btnRemoveFilter.Enabled = true;
        }

        private void btnUniqueRequest_Click(object sender, EventArgs e)
        {
            isUniqueIPactive = UniqueIPstate.notActive; if (x == null) return;

            dg.DataSource = null;
            dg.Rows.Clear();
            dg.Columns.Clear();

            dg.DataSource = new SortableBindingList<ApacheAccessModel>(x.AsEnumerable().GroupBy(log => log.request)
            .Select(group => group.First())
            .OrderBy(log => log.country)
            .ToList());

            this.Text = Application.ProductName + " - rows : " + dg.RowCount;
            btnRemoveFilter.Enabled = true;
        }

        private void btnUniqueCountry_Click(object sender, EventArgs e)
        {
            isUniqueIPactive = UniqueIPstate.notActive; if (x == null) return;

            dg.DataSource = null;
            dg.Rows.Clear();
            dg.Columns.Clear();

            dg.DataSource = new SortableBindingList<ApacheAccessModel>(x.AsEnumerable().GroupBy(log => log.country)
            .Select(group => group.First())
            .OrderBy(log => log.country)
            .ToList());

            this.Text = Application.ProductName + " - rows : " + dg.RowCount;
            btnRemoveFilter.Enabled = true;
        }

        private void dg_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                btnNftables.Enabled = isUniqueIPactive == UniqueIPstate.IP;
                btnNftablesRange.Enabled = isUniqueIPactive == UniqueIPstate.IPrange;

                ctx.Show(System.Windows.Forms.Cursor.Position);
            }
        }

        private void btnNftables_Click(object sender, EventArgs e)
        {
            string template = "                ip saddr {0} drop";

            StringBuilder sb = new StringBuilder();

            int destCell = (isUniqueIPactive == UniqueIPstate.IP ? 0 : 6);
            foreach (DataGridViewRow r in dg.Rows)
            {
                sb.AppendLine(string.Format(template, r.Cells[destCell].Value.ToStrinX()));
            }

            General.Copy2Clipboard(sb.ToString());
        }

        private void btnRemoveFilter_Click(object sender, EventArgs e)
        {
            isUniqueIPactive = UniqueIPstate.notActive; if (x == null) return;

            dg.DataSource = null;
            dg.Rows.Clear();
            dg.Columns.Clear();

            dg.DataSource = (x);

            this.Text = Application.ProductName + " - rows : " + dg.RowCount;
            btnRemoveFilter.Enabled = false;
        }

        private void btnCheckOnline_Click(object sender, EventArgs e)
        {
            string domain = string.Empty;
            switch ((sender as ToolStripDropDownItem).Name)
            {
                case "btnCheckOnlineMyIP":
                    domain = "https://myip.ms/info/whois/";
                    break;
                case "btnCheckOnlineIP2location":
                    domain = "https://www.ip2location.com/demo/";
                    break;
                case "btnCheckOnlinedMaxmind":
                    domain = "https://www.maxmind.com/en/geoip-demo/#";
                    break;
            }

            if (dg.SelectedCells[0].Value != null)
            {
                int r = dg.SelectedCells[0].RowIndex;
                System.Diagnostics.Process.Start(domain + dg.Rows[r].Cells[0].Value.ToStrinX());
            }
        }

        private void dg_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))

                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void dg_DragDrop(object sender, DragEventArgs e)
        {
            string[] fl = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (fl[0].ToLower().EndsWith(".log"))
                LoadData2Grid(File.ReadAllText(fl[0]));
        }

        private void dg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dg.Rows.Count != x.Count)
                {
                   General.Mes("You can use the 'delete functionality', only when rows are unfiltered.");
                    return;
                }

                var uniqueRowIndices = dg.SelectedCells
                                     .Cast<DataGridViewCell>() // Cast to DataGridViewCell
                                     .Select(cell => cell.RowIndex) // Select the RowIndex
                                     .Distinct() // Get unique RowIndices
                                     .OrderByDescending(index => index) // Sort in descending order
                                     .ToList(); // Convert to a list

                foreach (int rowIndex in uniqueRowIndices)
                {
                    x.Remove((ApacheAccessModel)dg.Rows[rowIndex].DataBoundItem);
                }
            }

            this.Text = Application.ProductName + " - rows : " + dg.RowCount;
        }
    }
}
