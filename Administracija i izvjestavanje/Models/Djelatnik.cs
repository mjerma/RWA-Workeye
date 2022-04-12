using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administracija_i_izvjestavanje.Models
{
    public class Djelatnik
    {
        public int IDDjelatnik { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Zaporka { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public int TipDjelatnikaID { get; set; }
        public int? TimID { get; set; }
        public bool JeAktivan { get; set; }
        public string PunoIme => ImePrezime();

        public string ImePrezime()
        {
            return $"{Ime} {Prezime}";
        }
    }
}