using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evidencija_radnih_sati.Models
{
    public class ProjektSatnica
    {
        public int IDProjektSatnica { get; set; }
        public string Zabiljezeno { get; set; }
        public int RadniSati { get; set; }
        public int PrekovremeniSati { get; set; }
        public int ProjektID { get; set; }
        public int SatnicaID { get; set; }
    }
}