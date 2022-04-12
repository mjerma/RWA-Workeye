using Administracija_i_izvjestavanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracija_i_izvjestavanje
{
    public partial class DodajKlijenta : MyPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["korisnik"] == null)
            {
                Response.Redirect("~/Prijava.aspx");
            }
        }

        protected void btnSpremi_Click(object sender, EventArgs e)
        {
            Klijent klijent = new Klijent
            {
                Naziv = txtNaziv.Text,
                Telefon = txtTelefon.Text,
                Email = txtEmail.Text
            };

            Repo.CreateKlijent(klijent);

            Response.Redirect("Klijenti.aspx");

        }

        protected void btnOdustani_Click(object sender, EventArgs e)
        {
            Response.Redirect("Klijenti.aspx");
        }
    }
}