using Administracija_i_izvjestavanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracija_i_izvjestavanje
{
    public partial class IzvjestajZaKlijentaForma : MyPage
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
            List<Klijent> klijenti = Repo.GetKlijenti();

            ddlKlijenti.DataSource = klijenti;
            ddlKlijenti.DataValueField = "IDKlijent";
            ddlKlijenti.DataTextField = "Naziv";
            ddlKlijenti.DataBind();

            var kuki = Request.Cookies["culture"];

            if (kuki != null)
            {
                switch (kuki.Value)
                {
                    case "en":
                        ddlKlijenti.Items.Insert(0, new ListItem("--Choose client--", "NA"));
                        break;
                    case "hr":
                        ddlKlijenti.Items.Insert(0, new ListItem("--Izaberite klijenta--", "NA"));
                        break;
                }
            }
        }

        protected void btnPrikazi_Click(object sender, EventArgs e)
        {
            int KlijentID = int.Parse(ddlKlijenti.SelectedValue);
            string klijentNaziv = ddlKlijenti.SelectedItem.Text;
            DateTime pocetniDatum = DateTime.Parse(txtPocetniDatum.Text);
            DateTime zavrsniDatum = DateTime.Parse(txtZavrsniDatum.Text);

            KlijentIzvjestaj izvjestaj = new KlijentIzvjestaj()
            {
                KlijentID = KlijentID,
                klijentNaziv = klijentNaziv,
                pocetniDatum = pocetniDatum,
                zavrsniDatum = zavrsniDatum
            };

            Session["IzvjestajZaKlijenta"] = izvjestaj;

            Response.Redirect("IzvjestajZaKlijenta.aspx");

        }
    }
}