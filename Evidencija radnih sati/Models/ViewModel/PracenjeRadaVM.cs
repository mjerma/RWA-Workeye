using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Evidencija_radnih_sati.Models.ViewModel
{
    public class PracenjeRadaVM
    {
        public int IDDjelatnik { get; set; }
        public string DjelatnikPunoIme { get; set; }
        public int IDSatnica { get; set; }
        public Satnica Satnica { get; set; }
        public List<Projekt> Projekti { get; set; }
        public DateTime Datum { get; set; }
        public bool PromjenaDatuma { get; set; }
        public string[] Zabiljezeno { get; set; }

        [Range(0,8, ErrorMessageResourceType = typeof(Resources.ErrorMsg), ErrorMessageResourceName = "RadniSati")]
        public int[] radniSati { get; set; }

        [Range(0,4, ErrorMessageResourceType = typeof(Resources.ErrorMsg), ErrorMessageResourceName = "PrekovremeniSati")]
        public int[] prekovremeniSati { get; set; }
        public string zabiljezenoUkupno { get; set; }

        [RegularExpression(@"^(8)$", ErrorMessageResourceType = typeof(Resources.ErrorMsg), ErrorMessageResourceName = "RadniSatiUkupno")]
        public int radniSatiUkupno { get; set; }

        [Range(0, 4, ErrorMessageResourceType = typeof(Resources.ErrorMsg), ErrorMessageResourceName = "PrekovremeniSatiUkupno")]
        public int prekovremeniSatiUkupno { get; set; }
        public string Komentar { get; set; }
    }
}