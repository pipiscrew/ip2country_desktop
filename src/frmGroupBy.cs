using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ip2country_desktop
{
    public partial class frmGroupBy : Form
    {
        public frmGroupBy(object groupedList, string field)
        {
            InitializeComponent();

            DrawingControl.SetDoubleBuffered(dg);

            this.Text = "Group by : " + field;

            DrawingControl.SuspendDrawing(dg);
        
            dg.DataSource = groupedList;

            dg.Columns[0].HeaderText = "title";
            dg.Columns[1].HeaderText = "count";

            DrawingControl.ResumeDrawing(dg);
        }
    }
}
