using Administracija_i_izvjestavanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracija_i_izvjestavanje
{
    public partial class DodajTim : MyPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnOdustani_Click(object sender, EventArgs e)
        {
            Response.Redirect("Timovi.aspx");
        }

        protected void btnSpremi_Click(object sender, EventArgs e)
        {
            Tim tim = new Tim()
            {
                Naziv = txtNaziv.Text,
                DatumKreiranja = DateTime.Now,
                JeAktivan = true
            };

            Repo.CreateTim(tim);

            Response.Redirect("Timovi.aspx");
        }
    }
}