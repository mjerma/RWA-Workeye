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
    public partial class Klijenti : MyPage
    {
        private List<Klijent> klijenti = Repo.GetKlijenti();

        private Djelatnik korisnik;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["korisnik"] == null)
            {
                Response.Redirect("~/Prijava.aspx");
            }

            korisnik = Session["korisnik"] as Djelatnik;
            ShowKlijenti();
        }

        private void ShowKlijenti()
        {
            List<string> headerRow = new List<string> { "IDKlijent", "Naziv", "Telefon" , "Email", "" };
            DataTable dt = new DataTable();
            headerRow.ForEach(h => dt.Columns.Add(h));

            for (int i = 0; i < klijenti.Count; i++)
            {
                dt.Rows.Add(klijenti[i].IDKlijent, klijenti[i].Naziv, klijenti[i].Telefon, klijenti[i].Email);
            }

            gvKlijenti.DataSource = dt;
            gvKlijenti.DataBind();
        }

        protected void gvKlijenti_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvKlijenti.PageIndex = e.NewPageIndex;
            this.ShowKlijenti();
        }

        protected void gvKlijenti_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int KlijentID = int.Parse(gvKlijenti.DataKeys[e.Row.RowIndex].Values[0].ToString());
                if (Repo.GetKlijent(KlijentID).JeAktivan)
                {
                    KlijentAktivan(e.Row);
                }
                else
                {
                    KlijentNijeAktivan(e.Row);
                }
            }
        }

        private void KlijentNijeAktivan(GridViewRow row)
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

            row.Cells[3].Controls.Add(btnUredi);
            row.Cells[3].Controls.Add(btnAktiviraj);
        }

        private void KlijentAktivan(GridViewRow row)
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

            if (korisnik.TipDjelatnikaID == 2)
            {
                btnUredi.Enabled = false;
                btnDeaktiviraj.Enabled = false;
            }

            row.Cells[3].Controls.Add(btnUredi);
            row.Cells[3].Controls.Add(btnDeaktiviraj);
        }

        private void BtnDeaktiviraj_Click(object sender, EventArgs e)
        {
            Button btnDeaktiviraj = sender as Button;
            GridViewRow row = btnDeaktiviraj.NamingContainer as GridViewRow;
            int DjelatnikID = int.Parse(gvKlijenti.DataKeys[row.RowIndex].Values[0].ToString());

            Repo.UpdateKlijentJeAktivan(DjelatnikID, false);
            row.Cells[3].Controls.Clear();
            KlijentNijeAktivan(row);
        }

        private void BtnAktiviraj_Click(object sender, EventArgs e)
        {

            Button btnAktiviraj = sender as Button;
            GridViewRow row = btnAktiviraj.NamingContainer as GridViewRow;
            int DjelatnikID = int.Parse(gvKlijenti.DataKeys[row.RowIndex].Values[0].ToString());

            Repo.UpdateKlijentJeAktivan(DjelatnikID, true);
            row.Cells[3].Controls.Clear();
            KlijentAktivan(row);
        }

        private void BtnUredi_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string KlijentID = gvKlijenti.DataKeys[row.RowIndex].Values[0].ToString();

            Session["klijent"] = klijenti.Find(k => k.IDKlijent == int.Parse(KlijentID));

            Response.Redirect("UrediKlijenta.aspx");
        }

        protected void btnDodajKlijenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("DodajKlijenta.aspx");
        }
    }
}