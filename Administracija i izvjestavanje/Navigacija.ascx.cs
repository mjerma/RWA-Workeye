using Administracija_i_izvjestavanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracija_i_izvjestavanje
{
    public partial class Navigacija : System.Web.UI.UserControl
    {
        public event EventHandler<NavigationEventArgs> OnLanguageChange;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnHR_Click(object sender, EventArgs e)
        {
            OnLanguageChange?.Invoke(this, new NavigationEventArgs { Culture = "hr" });
        }

        protected void btnEN_Click(object sender, EventArgs e)
        {
            OnLanguageChange?.Invoke(this, new NavigationEventArgs { Culture = "en" });
        }
    }
}