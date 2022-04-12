using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administracija_i_izvjestavanje.Models
{
    public class Klijent
    {
        public int IDKlijent { get; set; }
        public string Naziv { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public bool JeAktivan { get; set; }
    }
}