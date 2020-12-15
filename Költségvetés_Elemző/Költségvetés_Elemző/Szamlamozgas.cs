using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Költségvetés_Elemző
{
    class Szamlamozgas
    {
        public DateTime könyvelés_dátuma { get; set; }
        public string tranzakció_azonosító { get; set; }
        public string típus { get; set; }
        public string könyvelési_számla { get; set; }
        public string könyvelési_számla_elnevezése { get; set; }
        public string partner_számla_elnevezése { get; set; }
        public double összeg { get; set; }
        public string deviza { get; set; }
    }
}
