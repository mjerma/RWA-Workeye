using Evidencija_radnih_sati.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Evidencija_radnih_sati
{
    public class Repo
    {
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        internal static Djelatnik PrijavaKorisnika(string email, string zaporka)
        {
            Djelatnik djelatnik = new Djelatnik();

            var data = SqlHelper.ExecuteDataset(cs, "PrijavaKorisnika", email, zaporka).Tables[0];

            if(data.Rows.Count > 0)
            {
                djelatnik = GetDjelatnikFromDataRow(data.Rows[0]);

                if (djelatnik.JeAktivan)
                {
                    return djelatnik;
                }
                return null;
            }
            return null;
        }

        internal static List<Djelatnik> GetDjelatnici()
        {
            List<Djelatnik> djelatnici = new List<Djelatnik>();

            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetDjelatnici").Tables[0].Rows)
            {
                djelatnici.Add(GetDjelatnikFromDataRow(row));
            }
            return djelatnici;
        }

        internal static Djelatnik GetDjelatnik(int djelatnikID)
        {
            Djelatnik djelatnik = new Djelatnik();

            var data = SqlHelper.ExecuteDataset(cs, "GetDjelatnik", djelatnikID).Tables[0];
            djelatnik = GetDjelatnikFromDataRow(data.Rows[0]);

            return djelatnik;
        }

        internal static List<ProjektSatnica> GetProjektiSatnice(int SatnicaID)
        {
            List<ProjektSatnica> projektiSatnice = new List<ProjektSatnica>();

            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetProjektiSatnice", SatnicaID).Tables[0].Rows)
            {
                projektiSatnice.Add(GetProjektSatnicaFromDataRow(row));
            }
            return projektiSatnice;

        }

        internal static ProjektSatnica GetProjektSatnicaFromDataRow(DataRow row)
        {
            ProjektSatnica projektSatnica = new ProjektSatnica();

            projektSatnica.IDProjektSatnica = int.Parse(row["IDProjektSatnica"].ToString());
            projektSatnica.Zabiljezeno = row["Zabiljezeno"].ToString();
            projektSatnica.RadniSati = int.Parse(row["RadniSati"].ToString());
            projektSatnica.PrekovremeniSati = int.Parse(row["PrekovremeniSati"].ToString());
            projektSatnica.ProjektID = int.Parse(row["ProjektID"].ToString());
            projektSatnica.SatnicaID = int.Parse(row["SatnicaID"].ToString());

            return projektSatnica;
        }

        internal static Satnica GetDjelatnikSatnica(DateTime datum, int DjelatnikID)
        {
            var data = SqlHelper.ExecuteDataset(cs, "GetDjelatnikSatnica", datum.Date, DjelatnikID).Tables[0];

            if (data.Rows.Count > 0)
            {
                Satnica satnica = new Satnica();
                satnica = GetSatnicaFromDataRow(data.Rows[0]);
                return satnica;
            }

            return null;
        }

        internal static Djelatnik GetVoditeljTima(int TimID)
        {
            Djelatnik voditelj = new Djelatnik();

            var data = SqlHelper.ExecuteDataset(cs, "GetVoditeljTima", TimID).Tables[0];

            if (data.Rows.Count > 0)
            {
                voditelj = GetDjelatnikFromDataRow(data.Rows[0]);
            }

            return voditelj;
        }

        internal static Djelatnik GetDjelatnikFromDataRow(DataRow row)
        {
            Djelatnik djelatnik = new Djelatnik();

            djelatnik.IDDjelatnik = int.Parse(row["IDDjelatnik"].ToString());
            djelatnik.Ime = row["Ime"].ToString();
            djelatnik.Prezime = row["Prezime"].ToString();
            djelatnik.Email = row["Email"].ToString();
            djelatnik.DatumZaposlenja = (DateTime)row["DatumZaposlenja"];
            djelatnik.TipDjelatnikaID = int.Parse(row["TipDjelatnikaID"].ToString());
            djelatnik.TimID = row.IsNull("TimID") ? default(int?) : int.Parse(row["TimID"].ToString());
            djelatnik.JeAktivan = bool.Parse(row["JeAktivan"].ToString());

            return djelatnik;
        }

        internal static Satnica GetSatnicaFromDataRow(DataRow row)
        {
            Satnica satnica = new Satnica();

            satnica.IDSatnica = int.Parse(row["IDSatnica"].ToString());
            satnica.DatumSatnice = (DateTime)row["DatumSatnice"];
            satnica.DatumSlanja = (DateTime)row["DatumSlanja"];
            satnica.ZabiljezenoUkupno = row["ZabiljezenoUkupno"].ToString();
            satnica.RadniSatiUkupno = int.Parse(row["RadniSatiUkupno"].ToString());
            satnica.PrekovremeniSatiUkupno = int.Parse(row["PrekovremeniSatiUkupno"].ToString());
            satnica.Komentar = row["Komentar"].ToString();
            satnica.DjelatnikID = int.Parse(row["DjelatnikID"].ToString());
            satnica.VoditeljID = int.Parse(row["VoditeljID"].ToString());
            satnica.JePoslano = bool.Parse(row["JePoslano"].ToString());
            satnica.JePotvrdeno = bool.Parse(row["JePotvrdeno"].ToString());

            return satnica;
        }

        internal static Djelatnik GetDirektor()
        {
            Djelatnik direktor = new Djelatnik();

            var data = SqlHelper.ExecuteDataset(cs, "GetDirektor").Tables[0];

            if (data.Rows.Count > 0)
            {
                direktor = GetDjelatnikFromDataRow(data.Rows[0]);
            }

            return direktor;
        }

        internal static void UpdateSatnica(Satnica satnica)
        {
            SqlHelper.ExecuteNonQuery(cs, "UpdateSatnica", satnica.IDSatnica, satnica.DatumSlanja, satnica.ZabiljezenoUkupno, 
                satnica.RadniSatiUkupno, satnica.PrekovremeniSatiUkupno, satnica.Komentar, satnica.VoditeljID, satnica.JePoslano, satnica.JePotvrdeno);
        }

        internal static int CreateSatnica(Satnica satnica)
        {
            int SatnicaID = (int)SqlHelper.ExecuteScalar(cs, "CreateSatnica", satnica.DatumSatnice, satnica.DatumSlanja, satnica.ZabiljezenoUkupno, satnica.RadniSatiUkupno,
                satnica.PrekovremeniSatiUkupno, satnica.Komentar, satnica.DjelatnikID, satnica.VoditeljID, satnica.JePoslano, satnica.JePotvrdeno);

            return SatnicaID;
        }

        internal static Satnica GetSatnica(int SatnicaID)
        {
            Satnica satnica = new Satnica();

            var data = SqlHelper.ExecuteDataset(cs, "GetSatnica", SatnicaID).Tables[0];

            if (data.Rows.Count > 0)
            {
                satnica = GetSatnicaFromDataRow(data.Rows[0]);
            }

            return satnica;
        }

        internal static List<Satnica> GetSatniceZaPotvrdu(int VoditeljID)
        {
            List<Satnica> satnice = new List<Satnica>();

            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetSatniceZaPotvrdu", VoditeljID).Tables[0].Rows)
            {
                satnice.Add(GetSatnicaFromDataRow(row));
            }
            return satnice;
        }

        internal static void UpdateProjektSatnica(ProjektSatnica projektSatnica)
        {
            SqlHelper.ExecuteNonQuery(cs, "UpdateProjektSatnica", projektSatnica.Zabiljezeno, projektSatnica.RadniSati, projektSatnica.PrekovremeniSati,
                projektSatnica.ProjektID, projektSatnica.SatnicaID);
        }

        internal static List<Projekt> GetDjelatnikProjekti(int DjelatnikID)
        {
            List<Projekt> projekti = new List<Projekt>();

            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetDjelatnikProjekti", DjelatnikID).Tables[0].Rows)
            {
                projekti.Add(GetProjektFromDataRow(row));
            }
            return projekti;
        }

        internal static void CreateProjektSatnica(ProjektSatnica projektSatnica)
        {
            SqlHelper.ExecuteNonQuery(cs, "CreateProjektSatnica", projektSatnica.Zabiljezeno, projektSatnica.RadniSati, projektSatnica.PrekovremeniSati, 
                projektSatnica.ProjektID, projektSatnica.SatnicaID);
        }

        internal static void UpdateSatnicaJePoslano(int IDSatnica, string komentar)
        {
            SqlHelper.ExecuteNonQuery(cs, "UpdateSatnicaJePoslano", IDSatnica, komentar);
        }

        internal static void SpremiSatnicu(int IDSatnica)
        {
            SqlHelper.ExecuteNonQuery(cs, "SpremiSatnicu", IDSatnica);
        }

        internal static Projekt GetProjektFromDataRow(DataRow row)
        {
            Projekt projekt = new Projekt();

            projekt.IDProjekt = int.Parse(row["IDProjekt"].ToString());
            projekt.Naziv = row["Naziv"].ToString();
            projekt.KlijentID = int.Parse(row["KlijentID"].ToString());
            projekt.DatumOtvaranja = (DateTime)row["DatumOtvaranja"];
            projekt.VoditeljProjektaID = int.Parse(row["VoditeljProjektaID"].ToString());
            projekt.JeAktivan = bool.Parse(row["JeAktivan"].ToString());

            return projekt;
        }

        internal static TipDjelatnika GetTipDjelatnika(int IDTipDjelatnika)
        {
            TipDjelatnika tipDjelatnika = new TipDjelatnika();

            var data = SqlHelper.ExecuteDataset(cs, "GetTipDjelatnika", IDTipDjelatnika).Tables[0];

            if (data.Rows.Count > 0)
            {
                tipDjelatnika.IDTipDjelatnika = int.Parse(data.Rows[0]["IDTipDjelatnika"].ToString());
                tipDjelatnika.Naziv = data.Rows[0]["Naziv"].ToString();
            }

            return tipDjelatnika;
        }

        internal static void UpdateZaporka(int DjelatnikID, string zaporka)
        {
            SqlHelper.ExecuteNonQuery(cs, "UpdateZaporka", DjelatnikID, zaporka);
        }

    }
}
