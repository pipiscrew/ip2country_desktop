using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ip2country_desktop
{
    internal static class General
    {

        #region " CLIPBOARD OPERATIONS"

        public static void Copy2Clipboard(string val)
        {
            try
            {
                Clipboard.Clear();
                Clipboard.SetDataObject(val, true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName);
            }
        }

        public static string GetFromClipboard()
        {
            try
            {
                return Clipboard.GetText().Trim();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName);
                return "";
            }
        }

        #endregion

        public static string ToStrinX(this object value)
        {
            string retvalue = "";

            if (value != null)
                retvalue = value.ToString();

            return retvalue;
        }

        public static DialogResult Mes(string descr, MessageBoxIcon icon = MessageBoxIcon.Information, MessageBoxButtons butt = MessageBoxButtons.OK)
        {
            if (descr.Length > 0)
                return MessageBox.Show(descr, Application.ProductName, butt, icon);
            else
                return DialogResult.OK;
        }
    }
}
