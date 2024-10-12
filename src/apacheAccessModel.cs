using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ip2country_desktop
{
    public class ApacheAccessModel
    {
        public string ip { get; set; }
        public string date { get; set; }
        public string request { get; set; }
        public string status { get; set; }
        public string size { get; set; }
        public string country { get; set; }
        public string iprange { get; set; }
        public string asn { get; set; }
        public string asno { get; set; }
    }
}
