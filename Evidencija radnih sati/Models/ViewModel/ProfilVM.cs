using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Evidencija_radnih_sati.Models.ViewModel
{
    public class ProfilVM
    {
        public Djelatnik djelatnik { get; set; }
        public string TipDjelatnika { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorMsg), ErrorMessageResourceName = "ZaporkaPrazna")]
        [RegularExpression("[a-zA-Z0-9]{3,20}", ErrorMessageResourceType = typeof(Resources.ErrorMsg), ErrorMessageResourceName = "ZaporkaRegex")]
        public string novaZaporka { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ErrorMsg), ErrorMessageResourceName = "ZaporkaPrazna")]
        [RegularExpression("[a-zA-Z0-9]{3,20}", ErrorMessageResourceType = typeof(Resources.ErrorMsg), ErrorMessageResourceName = "ZaporkaRegex")]
        [Compare("novaZaporka", ErrorMessageResourceType = typeof(Resources.ErrorMsg), ErrorMessageResourceName = "ZaporkeNisuJednake")]
        public string ponoviNovuZaporku { get; set; }
    }
}