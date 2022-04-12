using Administracija_i_izvjestavanje.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracija_i_izvjestavanje
{
    public partial class Projekti : MyPage
    {
        List<Projekt> projekti = Repo.GetProjekti();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["korisnik"] == null)
            {
                Response.Redirect("~/Prijava.aspx");
            }
            ShowProjekti();
        }

        private void ShowProjekti()
        {
            List<string> headerRow = new List<string> { "IDProjekt", "Naziv", "Voditelj", "Klijent", "DatumKreiranja", "" };
            DataTable dt = new DataTable();
            headerRow.ForEach(h => dt.Columns.Add(h));

            for (int i = 0; i < projekti.Count; i++)
            {
                string voditelj = Repo.GetDjelatnik(projekti[i].VoditeljProjektaID).ImePrezime();
                string klijent = Repo.GetKlijent(projekti[i].KlijentID).Naziv;

                dt.Rows.Add(projekti[i].IDProjekt, projekti[i].Naziv, voditelj, klijent, projekti[i].DatumOtvaranja.ToShortDateString());
            }

            gvProjekti.DataSource = dt;
            gvProjekti.DataBind();
        }

        protected void gvProjekti_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProjekti.PageIndex = e.NewPageIndex;
            this.ShowProjekti();
        }

        protected void gvProjekti_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ProjektID = int.Parse(gvProjekti.DataKeys[e.Row.RowIndex].Values[0].ToString());
                if (Repo.GetProjekt(ProjektID).JeAktivan)
                {
                    ProjektAktivan(e.Row);
                }
                else
                {
                    ProjektNijeAktivan(e.Row);
                }
            }
        }

        private void ProjektNijeAktivan(GridViewRow row)
        {
            Button btnUredi = new Button();
            btnUredi.CssClass = "btn btn-secondary";
            btnUredi.ID = "btnUredi";

            Button btnAktiviraj = new Button();
            btnAktiviraj.CssClass = "btn btn-success";
            btnAktiviraj.ID = "btnAktiviraj";

            btnAktiviraj.Click += BtnAktiviraj_Click;

            var kuki = Request.Cookies["culture"];
            if (kuki != null)
            {
                switch (kuki.Value)
                {
                    case "en":
                        btnUredi.Text = "Update";
                        btnAktiviraj.Text = "Activate";
                        break;
                    case "hr":
                        btnUredi.Text = "Uredi";
                        btnAktiviraj.Text = "Aktiviraj";
                        break;
                }
            }
            else
            {
                btnUredi.Text = "Uredi";
                btnAktiviraj.Text = "Aktiviraj";
            }

            row.Cells[4].Controls.Add(btnUredi);
            row.Cells[4].Controls.Add(btnAktiviraj);
        }

        private void ProjektAktivan(GridViewRow row)
        {
            Button btnUredi = new Button();
            btnUredi.Text = "Uredi";
            btnUredi.CssClass = "btn btn-primary";
            btnUredi.ID = "btnUredi";

            Button btnDeaktiviraj = new Button();
            btnDeaktiviraj.Text = "Deaktiviraj";
            btnDeaktiviraj.CssClass = "btn btn-danger";
            btnDeaktiviraj.ID = "btnDeaktiviraj";

            btnUredi.Click += BtnUredi_Click;
            btnDeaktiviraj.Click += BtnDeaktiviraj_Click;

            var kuki = Request.Cookies["culture"];
            if (kuki != null)
            {
                switch (kuki.Value)
                {
                    case "en":
                        btnUredi.Text = "Update";
                        btnDeaktiviraj.Text = "Deactivate";
                        break;
                    case "hr":
                        btnUredi.Text = "Uredi";
                        btnDeaktiviraj.Text = "Deaktiviraj";
                        break;
                }
            }
            else
            {
                btnUredi.Text = "Uredi";
                btnDeaktiviraj.Text = "Deaktiviraj";
            }

            row.Cells[4].Controls.Add(btnUredi);
            row.Cells[4].Controls.Add(btnDeaktiviraj);
        }
        private void BtnDeaktiviraj_Click(object sender, EventArgs e)
        {
            Button btnDeaktiviraj = sender as Button;
            GridViewRow row = btnDeaktiviraj.NamingContainer as GridViewRow;
            int ProjektID = int.Parse(gvProjekti.DataKeys[row.RowIndex].Values[0].ToString());

            Repo.UpdateProjektJeAktivan(ProjektID, false);
            row.Cells[4].Controls.Clear();
            ProjektNijeAktivan(row);
        }

        private void BtnAktiviraj_Click(object sender, EventArgs e)
        {

            Button btnAktiviraj = sender as Button;
            GridViewRow row = btnAktiviraj.NamingContainer as GridViewRow;
            int ProjektID = int.Parse(gvProjekti.DataKeys[row.RowIndex].Values[0].ToString());

            Repo.UpdateProjektJeAktivan(ProjektID, true);
            row.Cells[4].Controls.Clear();
            ProjektAktivan(row);
        }
        private void BtnUredi_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string ProjektID = gvProjekti.DataKeys[row.RowIndex].Values[0].ToString();

            Session["projekt"] = projekti.Find(p => p.IDProjekt == int.Parse(ProjektID));

            Response.Redirect("UrediProjekt.aspx");
        }

        protected void btnDodajProjekt_Click(object sender, EventArgs e)
        {
            Response.Redirect("DodajProjekt.aspx");
        }
    }
}