using Administracija_i_izvjestavanje.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracija_i_izvjestavanje
{
    public partial class IzvjestajZaKlijenta : MyPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IzvjestajZaKlijenta"] == null)
            {
                Response.Redirect("IzvjestajZaKlijentaForma.aspx");
            }

            ShowIzvjestaj();
        }

        private void ShowIzvjestaj()
        {
            KlijentIzvjestaj info = Session["IzvjestajZaKlijenta"] as KlijentIzvjestaj;

            klijentHeader.InnerText = info.klijentNaziv;
            izvjestajPeriod.InnerText = info.pocetniDatum.ToShortDateString() + " - " + info.zavrsniDatum.ToShortDateString();

            KlijentIzvjestaj izvjestaj = Repo.GetIzvjestajZaKlijenta(info.KlijentID, info.pocetniDatum, info.zavrsniDatum);

            var kuki = Request.Cookies["culture"];

            //Ispis js alerta u slucaju da nema satnica za odabrani period
            if (izvjestaj == null)
            {
                if (kuki != null)
                {
                    switch (kuki.Value)
                    {
                        case "en":
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('No timesheet for selected period'); window.location='"
                            + Request.ApplicationPath + "IzvjestajZaKlijentaForma.aspx';", true);
                            return;
                        case "hr":
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Poruka", "alert('Nema satnica za zadani period'); window.location='"
                            + Request.ApplicationPath + "IzvjestajZaKlijentaForma.aspx';", true);
                            return;
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Poruka", "alert('Nema satnica za zadani period'); window.location='"
                            + Request.ApplicationPath + "IzvjestajZaKlijentaForma.aspx';", true);
                return;
            }

            izvjestaj.pocetniDatum = info.pocetniDatum;
            izvjestaj.zavrsniDatum = info.zavrsniDatum;

            List<string> headerRow = new List<string> { "Naziv projekta", "Sati" };
            DataTable dt = new DataTable();
            headerRow.ForEach(h => dt.Columns.Add(h));
            int ukupnoSati = 0;

            for (int i = 0; i < izvjestaj.naziviProjekta.Count; i++)
            {
                dt.Rows.Add(izvjestaj.naziviProjekta[i], izvjestaj.Sati[i]);
                ukupnoSati += izvjestaj.Sati[i];
            }

            if (kuki != null)
            {
                switch (kuki.Value)
                {
                    case "en":
                        dt.Rows.Add("Total", ukupnoSati);
                        break;
                    case "hr":
                        dt.Rows.Add("Ukupno", ukupnoSati);
                        break;
                }
            }

            gvIzvjestaj.DataSource = dt;
            gvIzvjestaj.DataBind();
        }

        protected void btnIzvoz_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=IzvjestajKlijentaExport.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";

            gvIzvjestaj.AllowPaging = false;
            gvIzvjestaj.DataBind();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < gvIzvjestaj.Columns.Count; i++)
            {
                if (i == gvIzvjestaj.Columns.Count - 1)
                {
                    sb.Append(gvIzvjestaj.Columns[i].HeaderText);
                }
                else
                {
                    sb.Append(gvIzvjestaj.Columns[i].HeaderText + ',');
                }
            }
            sb.Append("\n");
            for (int i = 0; i < gvIzvjestaj.Rows.Count; i++)
            {
                for (int j = 0; j < gvIzvjestaj.Columns.Count; j++)
                {
                    if (j == gvIzvjestaj.Columns.Count - 1)
                    {
                        sb.Append(gvIzvjestaj.Rows[i].Cells[j].Text);
                    }
                    else
                    {
                        sb.Append(gvIzvjestaj.Rows[i].Cells[j].Text + ',');
                    }
                }
                sb.Append("\n");
            }
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }
    }
}