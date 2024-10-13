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

            //DrawingControl.SetDoubleBuffered(this);
            DrawingControl.SetDoubleBuffered(dg);

            //add class properties to toolbar 'groupby' dropdown
            var properties = typeof(ApacheAccessModel).GetProperties();
            foreach (var prop in properties)
            {
                btnGroupBy.DropDown.Items.Add(prop.Name);
                btnGroupBy.DropDown.Items[btnGroupBy.DropDown.Items.Count - 1].Click += btnGroupByDropdown_Click;
            }
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
            Cursor = System.Windows.Forms.Cursors.WaitCursor;

            if (dbCountry == null)
                if (File.Exists("GeoLite2-Country.mmdb"))
                    dbCountry = new DatabaseReader("GeoLite2-Country.mmdb");

            if (dbASN == null)
                if (File.Exists("GeoLite2-ASN.mmdb"))
                    dbASN = new DatabaseReader("GeoLite2-ASN.mmdb");

            DrawingControl.SuspendDrawing(dg);

            dg.DataSource = null;
            dg.Rows.Clear();
            dg.Columns.Clear();
            btnRemoveFilter.Enabled = false; isUniqueIPactive = UniqueIPstate.notActive;

            string patternIP = @"\b(?:\d{1,3}\.){3}\d{1,3}\b";
            string patternLine = @"^(?<ip>.*?) - - (?<dt>.*?) ""(?<request>.*?)"" (?<status>.*?) (?<size>.*?)$";

            string s2;

            x = new SortableBindingList<ApacheAccessModel>();
            Regex regex = new Regex(patternLine, RegexOptions.Compiled);
            Match match;

            ApacheAccessModel y;
            string[] country; string[] asn;
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
                        asn = GetASNByIP(y.ip);
                        y.asn = asn[0];
                        y.asno = asn[1];
                    }
                }
                else
                {
                    match = new Regex(patternIP).Match(s2);
                    if (match.Success)
                    {
                        y.ip = match.Value;
                        country = GetCountryByIP(y.ip);
                        y.country = country[0];
                        y.iprange = country[1];
                        asn = GetASNByIP(y.ip);
                        y.asn = asn[0];
                        y.asno = asn[1];
                    }
                    else
                        y.ip = null;
                }

                x.Add(y);
            }

            dg.DataSource = x;
            DrawingControl.ResumeDrawing(dg);

            this.Text = Application.ProductName + " - rows : " + dg.RowCount;

            Cursor = System.Windows.Forms.Cursors.Default;
        }

        private static DatabaseReader dbCountry = null;
        private static string[] GetCountryByIP(string ip)
        {
            if (dbCountry == null)
                return new string[] { "dbase", "no found" };

            try
            {
                CountryResponse data = dbCountry.Country(ip);

                if (data.Country.IsoCode == null)
                {
                    if (data.Continent != null && data.Traits.Network.NetworkAddress != null && data.Traits.Network.PrefixLength != null)
                        return new string[] { data.Continent.Name, data.Traits.Network.NetworkAddress + "/" + data.Traits.Network.PrefixLength.ToString() };

                    if (data.Continent != null)
                        return new string[] { data.Continent.Name, "" };

                    return new string[] { "N/A", "N/A" };
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

                return new string[] { "N/A", "N/A" };
            }
        }

        private static DatabaseReader dbASN = null;
        private static string[] GetASNByIP(string ip)
        {
            if (dbASN == null)
                return new string[] { "dbase", "no found" };

            try
            {
                AsnResponse responseASN;

                if (dbASN.TryAsn(ip, out responseASN))
                    return new string[] { responseASN.AutonomousSystemOrganization, responseASN.AutonomousSystemNumber.ToStrinX() };
                else
                    return new string[] { "N/A", "N/A" };

            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException || ex is System.IO.FileNotFoundException)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }

                return new string[] { "N/A", "N/A" };
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
            if (e.Button == System.Windows.Forms.MouseButtons.Right && dg.SelectedCells.Count > 0)
            {
                btnNftables.Enabled = isUniqueIPactive == UniqueIPstate.IP;
                btnNftablesRange.Enabled = isUniqueIPactive == UniqueIPstate.IPrange;
                
                string asn = dg.Rows[dg.SelectedCells[0].RowIndex].Cells[7].Value.ToStrinX();
                btnRemoveASN.Text = string.Format("remove asn '{0}'", asn);
                btnRemoveASN.Tag = dg.Rows[dg.SelectedCells[0].RowIndex].DataBoundItem;

                string country = dg.Rows[dg.SelectedCells[0].RowIndex].Cells[5].Value.ToStrinX();
                btnRemoveCountry.Text = string.Format("remove country '{0}'", country);
                btnRemoveCountry.Tag = dg.Rows[dg.SelectedCells[0].RowIndex].DataBoundItem;

                ctx.Show(System.Windows.Forms.Cursor.Position);
            }
        }

        private void btnNftables_Click(object sender, EventArgs e)
        {
            //string template = "                ip saddr {0} drop #{1}";
            string template = "\t\t\t\t\t\t{0}, #{1}";

            StringBuilder sbIPv4 = new StringBuilder();
            StringBuilder sbIPv6 = new StringBuilder();

            int destCell = (isUniqueIPactive == UniqueIPstate.IP ? 0 : 6);

            string iprange = string.Empty;
            foreach (DataGridViewRow r in dg.Rows)
            {
                iprange = r.Cells[6].Value.ToStrinX();
                if (iprange.Contains("::"))
                    sbIPv6.AppendLine(string.Format(template, r.Cells[destCell].Value.ToStrinX(), r.Cells[7].Value.ToStrinX()));
                else
                    sbIPv4.AppendLine(string.Format(template, r.Cells[destCell].Value.ToStrinX(), r.Cells[7].Value.ToStrinX()));
            }

            General.Copy2Clipboard(sbIPv4.Append(sbIPv6).ToString());
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


        private void btnGroupByDropdown_Click(object sender, EventArgs e)
        {
            if (x == null)
                return;

            string selectedProperty = (sender as ToolStripDropDownItem).Text;
            //var groupedData = x.GroupBy(y => y.GetType().GetProperty(selectedProperty).GetValue(y, null))
            //    .Select(g => new 
            //    {
            //        Key = g.Key,
            //        Count = g.Count()
            //    })
            //    .ToList();

            var groupedData = (from y in x
                               group y by y.GetType().GetProperty(selectedProperty).GetValue(y, null) into g
                               orderby g.Count() descending
                               select new Tuple<object, int>(g.Key, g.Count())).ToList();

            frmGroupBy z = new frmGroupBy(new SortableBindingList<Tuple<object, int>>(groupedData), selectedProperty);
            z.Show();
        }
        
        private void btnRemoveASN_Click(object sender, EventArgs e)
        {
            if (btnRemoveASN.Tag == null)
                return;

            RemoveWhere(7, (btnRemoveASN.Tag as ApacheAccessModel).asn);
        }

        private void btnRemoveCountry_Click(object sender, EventArgs e)
        {
            if (btnRemoveCountry.Tag == null)
                return;

            RemoveWhere(5, (btnRemoveCountry.Tag as ApacheAccessModel).country);
        }

        private void RemoveWhere(int dgColIndex , string item)
        {
            if (dg.Rows.Count != x.Count)
            {
                General.Mes("You can use the 'remove functionality', only when rows are unfiltered.");
                return;
            }

            Cursor = System.Windows.Forms.Cursors.WaitCursor;

            var rows = (from DataGridViewRow row in dg.Rows
                        where row.Cells[dgColIndex].Value != null && row.Cells[dgColIndex].Value.Equals(item)
                        select row.Index
                        ).OrderByDescending(index => index).ToList();

            DrawingControl.SuspendDrawing(dg);

            foreach (int rowIndex in rows)
            {
                x.RemoveAt(rowIndex);//((ApacheAccessModel)dg.Rows[rowIndex].DataBoundItem);
            }

            DrawingControl.ResumeDrawing(dg);

            this.Text = Application.ProductName + " - rows : " + dg.RowCount;

            Cursor = System.Windows.Forms.Cursors.Default;
        }
    }
}
