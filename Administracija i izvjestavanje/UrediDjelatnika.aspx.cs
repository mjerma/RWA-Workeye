using Administracija_i_izvjestavanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracija_i_izvjestavanje
{
    public partial class UrediDjelatnika : MyPage
    {
        public Djelatnik djelatnik { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["korisnik"] == null)
            {
                Response.Redirect("~/Prijava.aspx");
            }

            if (Session["djelatnik"] == null)
            {
                Response.Redirect("~/Djelatnici.aspx");
            }

            if (!IsPostBack)
            {
                ShowDjelatnik();
            }
        }
        private void ShowDjelatnik()
        {
            formPromjeniZaporku.Visible = false;

            djelatnik = Session["djelatnik"] as Djelatnik;

            List<TipDjelatnika> tipoviDjelatnika = Repo.GetTipoviDjelatnika();
            tipoviDjelatnika.Remove(tipoviDjelatnika.Single(x => x.Naziv == "Direktor"));

            List<Tim> timovi = Repo.GetTimovi();
            List<Projekt> projekti = Repo.GetProjekti();
            List<Projekt> DjelatnikProjekti = Repo.GetDjelatnikProjekti(djelatnik.IDDjelatnik);

            var kuki = Request.Cookies["culture"];

            txtIme.Text = djelatnik.Ime;
            txtPrezime.Text = djelatnik.Prezime;
            txtEmail.Text = djelatnik.Email;

            ddlTipDjelatnika.DataSource = tipoviDjelatnika;
            ddlTipDjelatnika.DataValueField = "IDTipDjelatnika";
            ddlTipDjelatnika.DataTextField = "Naziv";
            ddlTipDjelatnika.DataBind();
            ddlTipDjelatnika.SelectedValue = djelatnik.TipDjelatnikaID.ToString();


            ddlTim.DataSource = timovi;
            ddlTim.DataValueField = "IDTim";
            ddlTim.DataTextField = "Naziv";
            ddlTim.DataBind();
            ddlTim.SelectedValue = djelatnik.TimID.ToString();

            ddlProjekti.DataSource = projekti;
            ddlProjekti.DataValueField = "IDProjekt";
            ddlProjekti.DataTextField = "Naziv";
            ddlProjekti.DataBind();

            lbProjekti.DataSource = DjelatnikProjekti;
            lbProjekti.DataValueField = "IDProjekt";
            lbProjekti.DataTextField = "Naziv";
            lbProjekti.DataBind();

            if (kuki != null)
            {
                switch (kuki.Value)
                {
                    case "en":
                        ddlProjekti.Items.Insert(0, new ListItem("--Choose project--", "NA"));
                        break;
                    case "hr":
                        ddlProjekti.Items.Insert(0, new ListItem("--Odabir projekta--", "NA"));
                        break;
                }
            }
            else
            {
                ddlProjekti.Items.Insert(0, new ListItem("--Odabir projekta--", "NA"));
            }
        }

        protected void btnOdustani_Click(object sender, EventArgs e)
        {
            Response.Redirect("Djelatnici.aspx");
        }

        protected void btnPromjeniZaporku_Click(object sender, EventArgs e)
        {
            formPromjeniZaporku.Visible = true;
        }

        protected void btnSpremi_Click(object sender, EventArgs e)
        {
            djelatnik = Session["djelatnik"] as Djelatnik;

            Djelatnik updDjelatnik = new Djelatnik()
            {
                IDDjelatnik = djelatnik.IDDjelatnik,
                Ime = txtIme.Text,
                Prezime = txtPrezime.Text,
                Email = txtEmail.Text,
                TipDjelatnikaID = int.Parse(ddlTipDjelatnika.SelectedValue),
                TimID = int.Parse(ddlTim.SelectedValue)
            };

            List<int> ProjektiID = new List<int>();

            if (lbProjekti.Items.Count > 0)
            {
                for (int i = 0; i < lbProjekti.Items.Count; i++)
                {
                    ProjektiID.Add(int.Parse(lbProjekti.Items[i].Value));
                }
            }

            Repo.UpdateDjelatnik(updDjelatnik, ProjektiID);

            Response.Redirect("Djelatnici.aspx");

        }

        protected void btnSpremiZaporku_Click(object sender, EventArgs e)
        {
            djelatnik = Session["djelatnik"] as Djelatnik;
            if(tbNovaZaporka.Text == tbPonoviNovuZaporku.Text)
            {
                Repo.UpdateZaporka(djelatnik.IDDjelatnik, tbNovaZaporka.Text);
                formPromjeniZaporku.Visible = false;
            }
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