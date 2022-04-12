using Administracija_i_izvjestavanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracija_i_izvjestavanje
{
    public partial class DodajProjekt : MyPage
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
            List<Djelatnik> voditelji = Repo.GetVoditelji();

            var kuki = Request.Cookies["culture"];

            ddlKlijenti.DataSource = klijenti;
            ddlKlijenti.DataValueField = "IDKlijent";
            ddlKlijenti.DataTextField = "Naziv";
            ddlKlijenti.DataBind();

            ddlVoditelji.DataSource = voditelji;
            ddlVoditelji.DataValueField = "IDDjelatnik";
            ddlVoditelji.DataTextField = "PunoIme";
            ddlVoditelji.DataBind();

            if (kuki != null)
            {
                switch (kuki.Value)
                {
                    case "en":
                        ddlKlijenti.Items.Insert(0, new ListItem("--Choose client--", "NA"));
                        ddlVoditelji.Items.Insert(0, new ListItem("--Choose leader--", "NA"));
                        break;
                    case "hr":
                        ddlKlijenti.Items.Insert(0, new ListItem("--Izaberite klijenta--", "NA"));
                        ddlVoditelji.Items.Insert(0, new ListItem("--Izaberite voditelja--", "NA"));
                        break;
                }
            }

        }

        protected void btnOdustani_Click(object sender, EventArgs e)
        {
            Response.Redirect("Projekti.aspx");
        }

        protected void btnSpremi_Click(object sender, EventArgs e)
        {
            Projekt projekt = new Projekt
            {
                Naziv = txtNaziv.Text,
                KlijentID = int.Parse(ddlKlijenti.SelectedValue),
                DatumOtvaranja = DateTime.Now,
                VoditeljProjektaID = int.Parse(ddlVoditelji.SelectedValue),
                JeAktivan = true
            };

            Repo.CreateProjekt(projekt);

            Response.Redirect("Projekti.aspx");
        }
    }
}