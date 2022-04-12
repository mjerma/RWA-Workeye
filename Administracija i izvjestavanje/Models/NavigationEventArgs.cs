using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administracija_i_izvjestavanje.Models
{
    public class NavigationEventArgs:EventArgs
    {
        public string Culture { get; set; }
    }
}