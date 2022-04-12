using Administracija_i_izvjestavanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracija_i_izvjestavanje
{
    public partial class DodajDjelatnika : MyPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadForm();
            }
            txtZaporka.Attributes["value"] = txtZaporka.Text;
        }

        private void LoadForm()
        {
            List<TipDjelatnika> tipoviDjelatnika = Repo.GetTipoviDjelatnika();
            tipoviDjelatnika.Remove(tipoviDjelatnika.Single(x => x.Naziv == "Direktor"));
            
            List<Tim> timovi = Repo.GetTimovi();
            List<Projekt> projekti = Repo.GetProjekti();

            var kuki = Request.Cookies["culture"];

            ddlTim.DataSource = timovi;
            ddlTim.DataValueField = "IDTim";
            ddlTim.DataTextField = "Naziv";
            ddlTim.DataBind();
            
            ddlTipDjelatnika.DataSource = tipoviDjelatnika;
            ddlTipDjelatnika.DataValueField = "IDTipDjelatnika";
            ddlTipDjelatnika.DataTextField = "Naziv";
            ddlTipDjelatnika.DataBind();
            

            ddlProjekti.DataSource = projekti;
            ddlProjekti.DataValueField = "IDProjekt";
            ddlProjekti.DataTextField = "Naziv";
            ddlProjekti.DataBind();

            if (kuki != null)
            {
                switch (kuki.Value)
                {
                    case "en":
                        ddlTim.Items.Insert(0, new ListItem("--Choose team--", "NA"));
                        ddlTipDjelatnika.Items.Insert(0, new ListItem("--Choose employee type--", "NA"));
                        ddlProjekti.Items.Insert(0, new ListItem("--Choose project--", "NA"));
                        break;
                    case "hr":
                        ddlTim.Items.Insert(0, new ListItem("--Izaberite tim--", "NA"));
                        ddlTipDjelatnika.Items.Insert(0, new ListItem("--Izaberite tip djelatnika--", "NA"));
                        ddlProjekti.Items.Insert(0, new ListItem("--Odabir projekta--", "NA"));
                        break;
                }
            }
            else
            {
                ddlTim.Items.Insert(0, new ListItem("--Izaberite tim--", "NA"));
                ddlTipDjelatnika.Items.Insert(0, new ListItem("--Izaberite tip djelatnika--", "NA"));
                ddlProjekti.Items.Insert(0, new ListItem("--Odabir projekta--", "NA"));
            }
        }

        protected void btnOdustani_Click(object sender, EventArgs e)
        {
            Response.Redirect("Djelatnici.aspx");
        }

        protected void btnSpremi_Click(object sender, EventArgs e)
        {
            Djelatnik djelatnik = new Djelatnik()
            {
                Ime = txtIme.Text,
                Prezime = txtPrezime.Text,
                Email = txtEmail.Text,
                Zaporka = txtZaporka.Text,
                TipDjelatnikaID = int.Parse(ddlTipDjelatnika.SelectedValue),
                DatumZaposlenja = DateTime.Now,
                TimID = int.Parse(ddlTim.SelectedValue),
                JeAktivan = true
            };

            List<int> ProjektiID = new List<int>();

            if (lbProjekti.Items.Count > 0)
            {
                for (int i = 0; i < lbProjekti.Items.Count; i++)
                {
                    ProjektiID.Add(int.Parse(lbProjekti.Items[i].Value));
                }
            }
            Repo.CreateDjelatnik(djelatnik, ProjektiID);

            Response.Redirect("Djelatnici.aspx");
        }

        protected void ddlProjekti_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem listItem = new ListItem() { Value = ddlProjekti.SelectedItem.Value, Text = ddlProjekti.SelectedItem.Text };
            if (!lbProjekti.Items.Contains(listItem))
            {
                lbProjekti.Items.Add(listItem);
            }
        }
    }
}