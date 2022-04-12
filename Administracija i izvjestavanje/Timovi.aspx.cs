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
    public partial class Timovi : MyPage
    {
        private readonly List<Tim> timovi = Repo.GetTimovi();
        private Djelatnik korisnik;
        private readonly int voditeljTipDjelatnikaID = 2;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["korisnik"] == null)
            {
                Response.Redirect("~/Prijava.aspx");
            }

            korisnik = Session["korisnik"] as Djelatnik;
            ShowTimovi();
        }

        private void ShowTimovi()
        {
            if (korisnik.TipDjelatnikaID == voditeljTipDjelatnikaID)
            {
                btnDodajTim.Enabled = false;
                btnDodajTim.CssClass += " disabled";
            }

            List<string> headerRow = new List<string> { "IDTim", "Naziv", "Voditelj", "DatumKreiranja", "" };
            DataTable dt = new DataTable();
            headerRow.ForEach(h => dt.Columns.Add(h));

            for (int i = 0; i < timovi.Count; i++)
            {
                Djelatnik voditelj = Repo.GetVoditeljTima(timovi[i].IDTim);

                dt.Rows.Add(timovi[i].IDTim, timovi[i].Naziv, voditelj.ImePrezime(), timovi[i].DatumKreiranja.ToShortDateString());
            }
            gvTimovi.DataSource = dt;
            gvTimovi.DataBind();
        }

        protected void gvTimovi_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTimovi.PageIndex = e.NewPageIndex;
            this.ShowTimovi();
        }

        protected void gvTimovi_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int TimID = int.Parse(gvTimovi.DataKeys[e.Row.RowIndex].Values[0].ToString());
                if (Repo.GetTim(TimID).JeAktivan)
                {
                    TimAktivan(e.Row);
                }
                else
                {
                    TimNijeAktivan(e.Row);
                }
            }
        }

        private void TimNijeAktivan(GridViewRow row)
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

        private void TimAktivan(GridViewRow row)
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
            int TimID = int.Parse(gvTimovi.DataKeys[row.RowIndex].Values[0].ToString());

            Repo.UpdateTimJeAktivan(TimID, false);
            row.Cells[3].Controls.Clear();
            TimNijeAktivan(row);
        }

        private void BtnAktiviraj_Click(object sender, EventArgs e)
        {

            Button btnAktiviraj = sender as Button;
            GridViewRow row = btnAktiviraj.NamingContainer as GridViewRow;
            int DjelatnikID = int.Parse(gvTimovi.DataKeys[row.RowIndex].Values[0].ToString());

            Repo.UpdateTimJeAktivan(DjelatnikID, true);
            row.Cells[3].Controls.Clear();
            TimAktivan(row);
        }

        private void BtnUredi_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string TimID = gvTimovi.DataKeys[row.RowIndex].Values[0].ToString();

            Session["tim"] = timovi.Find(d => d.IDTim == int.Parse(TimID));

            Response.Redirect("UrediTim.aspx");
        }

        protected void btnDodajTim_Click(object sender, EventArgs e)
        {
            Response.Redirect("DodajTim.aspx");
        }
    }
}