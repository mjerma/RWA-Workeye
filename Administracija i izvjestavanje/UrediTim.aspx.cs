using Administracija_i_izvjestavanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracija_i_izvjestavanje
{
    public partial class UrediTim : MyPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["korisnik"] == null)
            {
                Response.Redirect("~/Prijava.aspx");
            }

            if (Session["tim"] == null)
            {
                Response.Redirect("~/Timovi.aspx");
            }

            if (!IsPostBack)
            {
                LoadForm();
            }
        }

        private void LoadForm()
        {
            Tim tim = Session["tim"] as Tim;

            txtNaziv.Text = tim.Naziv;
        }

        protected void btnOdustani_Click(object sender, EventArgs e)
        {
            Response.Redirect("Timovi.aspx");
        }

        protected void btnSpremi_Click(object sender, EventArgs e)
        {
            Tim tim = Session["tim"] as Tim;
            Tim updTim = new Tim
            {
                IDTim = tim.IDTim,
                Naziv = txtNaziv.Text,
                DatumKreiranja = tim.DatumKreiranja,
                JeAktivan = tim.JeAktivan
            };

            Repo.UpdateTim(updTim);

            Response.Redirect("Timovi.aspx");
        }
    }
}