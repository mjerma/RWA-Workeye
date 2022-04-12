using Administracija_i_izvjestavanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracija_i_izvjestavanje
{
    public partial class UrediProjekt : MyPage
    {
        public Projekt projekt { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["korisnik"] == null)
            {
                Response.Redirect("~/Prijava.aspx");
            }

            if (Session["projekt"] == null)
            {
                Response.Redirect("~/Projekti.aspx");
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

            projekt = Session["projekt"] as Projekt;

            txtNaziv.Text = projekt.Naziv;

            ddlKlijenti.DataSource = klijenti;
            ddlKlijenti.DataValueField = "IDKlijent";
            ddlKlijenti.DataTextField = "Naziv";
            ddlKlijenti.DataBind();
            ddlKlijenti.SelectedValue = projekt.KlijentID.ToString();

            ddlVoditelji.DataSource = voditelji;
            ddlVoditelji.DataValueField = "IDDjelatnik";
            ddlVoditelji.DataTextField = "PunoIme";
            ddlVoditelji.DataBind();
            ddlVoditelji.SelectedValue = projekt.VoditeljProjektaID.ToString();

        }

        protected void btnOdustani_Click(object sender, EventArgs e)
        {
            Response.Redirect("Projekti.aspx");
        }

        protected void btnSpremi_Click(object sender, EventArgs e)
        {
            projekt = Session["projekt"] as Projekt;

            Projekt updProjekt = new Projekt()
            {
                IDProjekt = projekt.IDProjekt,
                Naziv = txtNaziv.Text,
                KlijentID = int.Parse(ddlKlijenti.SelectedValue),
                VoditeljProjektaID = int.Parse(ddlVoditelji.SelectedValue)
            };

            Repo.UpdateProjekt(updProjekt);

            Response.Redirect("Projekti.aspx");

        }
    }
}