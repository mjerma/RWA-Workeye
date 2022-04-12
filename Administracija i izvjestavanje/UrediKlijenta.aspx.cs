using Administracija_i_izvjestavanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracija_i_izvjestavanje
{
    public partial class UrediKlijenta : MyPage
    {
        public Klijent klijent { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["korisnik"] == null)
            {
                Response.Redirect("~/Prijava.aspx");
            }

            if (Session["klijent"] == null)
            {
                Response.Redirect("~/Klijenti.aspx");
            }

            if (!IsPostBack)
            {
                ShowKlijentInfo();
            }
        }

        private void ShowKlijentInfo()
        {
            klijent = Session["klijent"] as Klijent;

            txtNaziv.Text = klijent.Naziv;
            txtTelefon.Text = klijent.Telefon;
            txtEmail.Text = klijent.Email;
        }

        protected void btnOdustani_Click(object sender, EventArgs e)
        {
            Response.Redirect("Klijenti.aspx");
        }

        protected void btnSpremi_Click(object sender, EventArgs e)
        {
            klijent = Session["klijent"] as Klijent;

            Klijent updKlijent = new Klijent
            {
                IDKlijent = klijent.IDKlijent,
                Naziv = txtNaziv.Text,
                Telefon = txtTelefon.Text,
                Email = txtEmail.Text
            };

            Repo.UpdateKlijent(updKlijent);

            Response.Redirect("Klijenti.aspx");
        }
    }
}