﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administracija_i_izvjestavanje.Models
{
    public class Tim
    {
        public int IDTim { get; set; }
        public string Naziv { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public bool JeAktivan { get; set; }
    }
}