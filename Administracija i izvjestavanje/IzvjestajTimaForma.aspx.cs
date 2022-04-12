using Administracija_i_izvjestavanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracija_i_izvjestavanje
{
    public partial class IzvjestajTimaForma : MyPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["korisnik"] == null)
            {
                Response.Redirect("~/Prijava.aspx");
            }

            if (!IsPostBack)
            {
                LoadForm();
            }
        }

        private void LoadForm()
        {
            List<Tim> timovi = Repo.GetTimovi();

            var kuki = Request.Cookies["culture"];

            ddlTim.DataSource = timovi;
            ddlTim.DataValueField = "IDTim";
            ddlTim.DataTextField = "Naziv";
            ddlTim.DataBind();

            if (kuki != null)
            {
                switch (kuki.Value)
                {
                    case "en":
                        ddlTim.Items.Insert(0, new ListItem("--Choose team--", "NA"));
                        break;
                    case "hr":
                        ddlTim.Items.Insert(0, new ListItem("--Izaberite tim--", "NA"));
                        break;
                }
            }
        }

        protected void btnPrikazi_Click(object sender, EventArgs e)
        {
            int TimID = int.Parse(ddlTim.SelectedValue);
            string timNaziv = ddlTim.SelectedItem.Text;
            DateTime pocetniDatum = DateTime.Parse(txtPocetniDatum.Text);
            DateTime zavrsniDatum = DateTime.Parse(txtZavrsniDatum.Text);

            TimIzvjestaj izvjestaj = new TimIzvjestaj()
            {
                TimID = TimID,
                timNaziv = timNaziv,
                pocetniDatum = pocetniDatum,
                zavrsniDatum = zavrsniDatum
            };

            Session["IzvjestajTima"] = izvjestaj;

            Response.Redirect("IzvjestajTima.aspx");

        }

    }
}