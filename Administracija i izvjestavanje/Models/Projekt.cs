﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administracija_i_izvjestavanje.Models
{
    public class Projekt
    {
        public int IDProjekt { get; set; }
        public string Naziv { get; set; }
        public int KlijentID { get; set; }
        public DateTime DatumOtvaranja { get; set; }
        public int VoditeljProjektaID { get; set; }
        public bool JeAktivan { get; set; }
    }
}