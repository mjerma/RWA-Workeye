using Administracija_i_izvjestavanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracija_i_izvjestavanje
{
    public partial class Prijava : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnPrijava_Click(object sender, EventArgs e)
        {
            Djelatnik korisnik = Repo.PrijavaKorisnika(tbEmail.Text, tbZaporka.Text);
            int direktorTipDjelatnikaID = 1;
            int voditeljTipDjelatnikaID = 2;

            if (korisnik != null && (korisnik.TipDjelatnikaID == direktorTipDjelatnikaID || 
                korisnik.TipDjelatnikaID == voditeljTipDjelatnikaID))
            {
                Session["korisnik"] = korisnik;
                Response.Redirect("Djelatnici.aspx");
            }
            else
            {
                lblPrijavaError.Text = "Neispravan email/zaporka ili nemate prava pristupa";
            }
        }
    }
}