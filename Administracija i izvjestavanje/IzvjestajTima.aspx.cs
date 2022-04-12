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
    public partial class IzvjestajTima : MyPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IzvjestajTima"] == null)
            {
                Response.Redirect("IzvjestajTimaForma.aspx");
            }

            ShowIzvjestaj();
        }

        private void ShowIzvjestaj()
        {
            TimIzvjestaj info = Session["IzvjestajTima"] as TimIzvjestaj;

            timHeader.InnerText = info.timNaziv;
            izvjestajPeriod.InnerText = info.pocetniDatum.ToShortDateString() + " - " + info.zavrsniDatum.ToShortDateString();

            TimIzvjestaj izvjestaj = Repo.GetIzvjestajTima(info.TimID, info.pocetniDatum, info.zavrsniDatum);

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
                            + Request.ApplicationPath + "IzvjestajTimaForma.aspx';", true);
                            return;
                        case "hr":
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Poruka", "alert('Nema satnica za zadani period'); window.location='"
                            + Request.ApplicationPath + "IzvjestajTimaForma.aspx';", true);
                            return;
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Poruka", "alert('Nema satnica za zadani period'); window.location='"
                            + Request.ApplicationPath + "IzvjestajTimaForma.aspx';", true);
                return;
            }

            izvjestaj.pocetniDatum = info.pocetniDatum;
            izvjestaj.zavrsniDatum = info.zavrsniDatum;

            List<string> headerRow = new List<string> { "Ime", "Prezime", "Tip djelatnika", "Redovni sati", "Prekovremeni sati", "Ukupno" };
            DataTable dt = new DataTable();
            headerRow.ForEach(h => dt.Columns.Add(h));

            int sumaRedovni = 0;
            int sumaPrekovremeni = 0;
            int sumaUkupno = 0;

            for (int i = 0; i < izvjestaj.djelatnici.Count; i++)
            {
                TipDjelatnika tipDjelatnika = Repo.GetTipDjelatnika(izvjestaj.djelatnici[i].TipDjelatnikaID);
                int ukupnoSati = izvjestaj.radniSati[i] + izvjestaj.prekovremeniSati[i];

                dt.Rows.Add(izvjestaj.djelatnici[i].Ime, izvjestaj.djelatnici[i].Prezime, tipDjelatnika.Naziv, 
                    izvjestaj.radniSati[i], izvjestaj.prekovremeniSati[i], ukupnoSati);

                sumaRedovni += izvjestaj.radniSati[i];
                sumaPrekovremeni += izvjestaj.prekovremeniSati[i];
                sumaUkupno += ukupnoSati;
            }

            if (kuki != null)
            {
                switch (kuki.Value)
                {
                    case "en":
                        dt.Rows.Add("Sum", " ", " ", sumaRedovni, sumaPrekovremeni, sumaUkupno);
                        break;
                    case "hr":
                        dt.Rows.Add("Suma", " ", " ", sumaRedovni, sumaPrekovremeni, sumaUkupno);
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
             "attachment;filename=IzvjestajTimaExport.csv");
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