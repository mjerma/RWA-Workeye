using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administracija_i_izvjestavanje.Models
{
    public class KlijentIzvjestaj
    {
        public int KlijentID { get; set; }
        public string klijentNaziv { get; set; }
        public DateTime pocetniDatum { get; set; }
        public DateTime zavrsniDatum { get; set; }
        public List<string> naziviProjekta { get; set; }
        public List<int> Sati { get; set; }

        public KlijentIzvjestaj()
        {
            naziviProjekta = new List<string>();
            Sati = new List<int>();
        }
    }
}