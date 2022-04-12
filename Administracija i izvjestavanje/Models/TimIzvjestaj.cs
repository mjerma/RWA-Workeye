using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administracija_i_izvjestavanje.Models
{
    public class TimIzvjestaj
    {
        public int TimID { get; set; }
        public string timNaziv { get; set; }
        public DateTime pocetniDatum { get; set; }
        public DateTime zavrsniDatum { get; set; }
        public List<Djelatnik> djelatnici { get; set; }
        public List<int> radniSati { get; set; }
        public List<int> prekovremeniSati { get; set; }

        public TimIzvjestaj()
        {
            djelatnici = new List<Djelatnik>();
            radniSati = new List<int>();
            prekovremeniSati = new List<int>();
        }
    }
}