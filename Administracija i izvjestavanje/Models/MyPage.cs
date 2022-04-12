using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;

namespace Administracija_i_izvjestavanje.Models
{
    public class MyPage : Page
    {
        protected override void InitializeCulture()
        {
            base.InitializeCulture();
            var kuki = Request.Cookies["culture"];
            if (kuki != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(kuki.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(kuki.Value);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("hr");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("hr");
            }
        }
    }
}