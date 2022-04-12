using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracija_i_izvjestavanje
{
    public partial class FormeEntitetaSite : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Navigacija.OnLanguageChange += Navigacija_OnLanguageChange;
        }

        private void Navigacija_OnLanguageChange(object sender, Models.NavigationEventArgs e)
        {
            HttpCookie kuki = new HttpCookie("culture");
            kuki.Value = e.Culture;
            Response.Cookies.Add(kuki);

            Response.Redirect(Request.Url.AbsolutePath);
        }
    }
}