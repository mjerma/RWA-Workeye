using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Administracija_i_izvjestavanje
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie kuki = Request.Cookies["culture"];
            if (kuki != null && kuki.Value != null)
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(kuki.Value);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(kuki.Value);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("hr");
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("hr");
            }
        }
    }
}