using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evidencija_radnih_sati.Models
{
    public class Satnica
    {
        public int IDSatnica { get; set; }
        public DateTime DatumSatnice { get; set; }
        public DateTime DatumSlanja { get; set; }
        public string ZabiljezenoUkupno { get; set; }
        public int RadniSatiUkupno { get; set; }
        public int PrekovremeniSatiUkupno { get; set; }
        public string Komentar { get; set; }
        public int DjelatnikID { get; set; }
        public int VoditeljID { get; set; }
        public bool JePoslano { get; set; }
        public bool JePotvrdeno { get; set; }
    }
}