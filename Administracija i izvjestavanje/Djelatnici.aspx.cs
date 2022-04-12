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
    public partial class Djelatnici : MyPage
    {
        private List<Djelatnik> djelatnici = Repo.GetDjelatnici();
        private Djelatnik korisnik;
        private readonly int voditeljTipDjelatnikaID = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["korisnik"] == null)
            {
                Response.Redirect("~/Prijava.aspx");
            }

            korisnik = Session["korisnik"] as Djelatnik;
            ShowDjelatnici();
        }

        private void ShowDjelatnici()
        {

            if (korisnik.TipDjelatnikaID == voditeljTipDjelatnikaID)
            {
                btnDodajDjelatnika.Enabled = false;
                btnDodajDjelatnika.CssClass += " disabled";
            }

            List<string> headerRow = new List<string> { "IDDjelatnik", "Ime", "Prezime", "E-mail", "Datum zaposlenja", "Tip djelatnika", "Tim", "" };
            DataTable dt = new DataTable();
            headerRow.ForEach(h => dt.Columns.Add(h));

            for (int i = 0; i < djelatnici.Count; i++)
            {
                string TipDjelatnika = Repo.GetTipDjelatnika(djelatnici[i].TipDjelatnikaID).Naziv;
                string tim = Repo.GetTim(djelatnici[i].TimID).Naziv;

                dt.Rows.Add(djelatnici[i].IDDjelatnik, djelatnici[i].Ime, djelatnici[i].Prezime, djelatnici[i].Email,
                            djelatnici[i].DatumZaposlenja.ToShortDateString(),
                            TipDjelatnika, tim);
            }
            gvDjelatnici.DataSource = dt;
            gvDjelatnici.DataBind();
        }

        protected void gvDjelatnici_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int DjelatnikID = int.Parse(gvDjelatnici.DataKeys[e.Row.RowIndex].Values[0].ToString());
                Djelatnik djelatnik = Repo.GetDjelatnik(DjelatnikID);

                if (djelatnik.JeAktivan)
                {
                    DjelatnikAktivan(e.Row, djelatnik);
                }
                else
                {
                    DjelatnikNijeAktivan(e.Row, djelatnik);
                }
            }
        }

        private void DjelatnikNijeAktivan(GridViewRow row, Djelatnik djelatnik)
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

            if (korisnik.TipDjelatnikaID == voditeljTipDjelatnikaID && djelatnik.TimID != korisnik.TimID)
            {
                btnUredi.Enabled = false;
                btnAktiviraj.Enabled = false;
            }

            row.Cells[6].Controls.Add(btnUredi);
            row.Cells[6].Controls.Add(btnAktiviraj);
        }

        private void DjelatnikAktivan(GridViewRow row, Djelatnik djelatnik)
        {
            Button btnUredi = new Button();
            btnUredi.CssClass = "btn btn-primary";
            btnUredi.ID = "btnUredi";

            Button btnDeaktiviraj = new Button();
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

            if (korisnik.TipDjelatnikaID == voditeljTipDjelatnikaID && djelatnik.TimID != korisnik.TimID)
            {
                btnUredi.Enabled = false;
                btnDeaktiviraj.Enabled = false;
            }

            row.Cells[6].Controls.Add(btnUredi);
            row.Cells[6].Controls.Add(btnDeaktiviraj);
        }

        private void BtnDeaktiviraj_Click(object sender, EventArgs e)
        {
            Button btnDeaktiviraj = sender as Button;
            GridViewRow row = btnDeaktiviraj.NamingContainer as GridViewRow;
            int DjelatnikID = int.Parse(gvDjelatnici.DataKeys[row.RowIndex].Values[0].ToString());

            Repo.UpdateDjelatnikJeAktivan(DjelatnikID, false);
            Djelatnik djelatnik = Repo.GetDjelatnik(DjelatnikID);
            row.Cells[6].Controls.Clear();
            DjelatnikNijeAktivan(row, djelatnik);
        }

        private void BtnAktiviraj_Click(object sender, EventArgs e)
        {

            Button btnAktiviraj = sender as Button;
            GridViewRow row = btnAktiviraj.NamingContainer as GridViewRow;
            int DjelatnikID = int.Parse(gvDjelatnici.DataKeys[row.RowIndex].Values[0].ToString());

            Repo.UpdateDjelatnikJeAktivan(DjelatnikID, true);
            Djelatnik djelatnik = Repo.GetDjelatnik(DjelatnikID);
            row.Cells[6].Controls.Clear();
            DjelatnikAktivan(row, djelatnik);
        }

        private void BtnUredi_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string DjelatnikID = gvDjelatnici.DataKeys[row.RowIndex].Values[0].ToString();

            Session["djelatnik"] = djelatnici.Find(d => d.IDDjelatnik == int.Parse(DjelatnikID));

            Response.Redirect("UrediDjelatnika.aspx");
        }

        protected void gvDjelatnici_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDjelatnici.PageIndex = e.NewPageIndex;
            this.ShowDjelatnici();
        }

        protected void btnDodajDjelatnika_Click(object sender, EventArgs e)
        {
            Response.Redirect("DodajDjelatnika.aspx");
        }
    }
}