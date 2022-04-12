using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Evidencija_radnih_sati.Models.ViewModel
{
    public class PrijavaVM
    {
        [Required(ErrorMessage = "Unesite email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Unesite zaporku")]
        public string Zaporka { get; set; }
        public string PrijavaError { get; set; }
    }
}