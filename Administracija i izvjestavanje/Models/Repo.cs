using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Administracija_i_izvjestavanje.Models
{
    public class Repo
    {
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        internal static Djelatnik PrijavaKorisnika(string email, string zaporka)
        {
            Djelatnik djelatnik = new Djelatnik();

            var data = SqlHelper.ExecuteDataset(cs, "PrijavaKorisnika", email, zaporka).Tables[0];

            if (data.Rows.Count > 0)
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

        internal static TimIzvjestaj GetIzvjestajTima(int TimID, DateTime pocetniDatum, DateTime zavrsniDatum)
        {
            TimIzvjestaj izvjestaj = new TimIzvjestaj();

            var data = SqlHelper.ExecuteDataset(cs, "GetIzvjestajTima", TimID, pocetniDatum, zavrsniDatum).Tables[0].Rows;

            if (data.Count > 0)
            {
                int DjelatnikID = 0;
                foreach (DataRow row in data)
                {
                    DjelatnikID = int.Parse(row["IDDjelatnik"].ToString());
                    Djelatnik djelatnik = GetDjelatnik(DjelatnikID);
                    izvjestaj.djelatnici.Add(djelatnik);
                    izvjestaj.radniSati.Add(int.Parse(row["RedovniRadniSati"].ToString()));
                    izvjestaj.prekovremeniSati.Add(int.Parse(row["PrekovremeniSati"].ToString()));
                }
                return izvjestaj;
            }
            return null;
        }

        internal static KlijentIzvjestaj GetIzvjestajZaKlijenta(int KlijentID, DateTime pocetniDatum, DateTime zavrsniDatum)
        {
            KlijentIzvjestaj izvjestaj = new KlijentIzvjestaj();

            var data = SqlHelper.ExecuteDataset(cs, "GetIzvjestajZaKlijenta", KlijentID, pocetniDatum, zavrsniDatum).Tables[0].Rows;

            if (data.Count > 0)
            {
                foreach (DataRow row in data)
                {
                    izvjestaj.naziviProjekta.Add(row["Naziv"].ToString());
                    izvjestaj.Sati.Add(int.Parse(row["Sati"].ToString()));
                }
                return izvjestaj;
            }
            
            return null;
        }

        public static List<Djelatnik> GetDjelatnici()
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

        public static List<Djelatnik> GetVoditelji()
        {
            List<Djelatnik> voditelji = new List<Djelatnik>();

            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetVoditelji").Tables[0].Rows)
            {
                voditelji.Add(GetDjelatnikFromDataRow(row));
            }
            return voditelji;
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

        public static List<Tim> GetTimovi()
        {
            List<Tim> timovi = new List<Tim>();

            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetTimovi").Tables[0].Rows)
            {
                timovi.Add(GetTimFromDataRow(row));
            }
            return timovi;
        }

        public static Tim GetTim(int? TimID)
        {
            Tim tim = new Tim();

            var data = SqlHelper.ExecuteDataset(cs, "GetTim", TimID).Tables[0];

            if (TimID == null)
            {
                tim.IDTim = 0;
                tim.Naziv = "";
                tim.DatumKreiranja = DateTime.MinValue;

                return tim;
            }

            tim = GetTimFromDataRow(data.Rows[0]);

            return tim;
        }

        public static List<TipDjelatnika> GetTipoviDjelatnika()
        {
            List<TipDjelatnika> tipoviDjelatnika = new List<TipDjelatnika>();

            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetTipoviDjelatnika").Tables[0].Rows)
            {
                tipoviDjelatnika.Add(GetTipDjelatnikaFromDataRow(row));
            }
            return tipoviDjelatnika;
        }

        public static TipDjelatnika GetTipDjelatnika(int TipDjelatnikaID)
        {
            TipDjelatnika tip = new TipDjelatnika();

            var data = SqlHelper.ExecuteDataset(cs, "GetTipDjelatnika", TipDjelatnikaID).Tables[0];

            tip = GetTipDjelatnikaFromDataRow(data.Rows[0]);

            return tip;

        }

        public static List<Klijent> GetKlijenti()
        {
            List<Klijent> klijenti = new List<Klijent>();

            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetKlijenti").Tables[0].Rows)
            {
                klijenti.Add(GetKlijentFromDataRow(row));
            }
            return klijenti;
        }

        public static Klijent GetKlijent(int KlijentID)
        {
            Klijent klijent = new Klijent();

            var data = SqlHelper.ExecuteDataset(cs, "GetKlijent", KlijentID).Tables[0];

            klijent = GetKlijentFromDataRow(data.Rows[0]);

            return klijent;
        }

        public static List<Projekt> GetProjekti()
        {
            List<Projekt> projekti = new List<Projekt>();

            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetProjekti").Tables[0].Rows)
            {
                projekti.Add(GetProjektFromDataRow(row));
            }
            return projekti;
        }

        public static Projekt GetProjekt(int projektID)
        {
            Projekt projekt = new Projekt();
            var data = SqlHelper.ExecuteDataset(cs, "GetProjekt", projektID).Tables[0];
            projekt = GetProjektFromDataRow(data.Rows[0]);
            return projekt;
        }

        private static Djelatnik GetDjelatnikFromDataRow(DataRow row)
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

        private static TipDjelatnika GetTipDjelatnikaFromDataRow(DataRow row)
        {
            TipDjelatnika tipDjelatnika = new TipDjelatnika();

            tipDjelatnika.IDTipDjelatnika = int.Parse(row["IDTipDjelatnika"].ToString());
            tipDjelatnika.Naziv = row["Naziv"].ToString();

            return tipDjelatnika;
        }

        private static Tim GetTimFromDataRow(DataRow row)
        {
            Tim tim = new Tim();

            tim.IDTim = int.Parse(row["IDTim"].ToString());
            tim.Naziv = row["Naziv"].ToString();
            tim.DatumKreiranja = (DateTime)row["DatumKreiranja"];
            tim.JeAktivan = bool.Parse(row["JeAktivan"].ToString());

            return tim;
        }

        public static void CreateDjelatnik(Djelatnik djelatnik, List<int> ProjektiID)
        {
            SqlParameter output = new SqlParameter("@DjelatnikID", SqlDbType.Int);
            output.Direction = ParameterDirection.Output;

            int DjelatnikID = (Int32)SqlHelper.ExecuteScalar(cs, "CreateDjelatnik", djelatnik.Ime, djelatnik.Prezime, djelatnik.Email, djelatnik.DatumZaposlenja,
                djelatnik.Zaporka, djelatnik.TipDjelatnikaID, djelatnik.TimID, djelatnik.JeAktivan);

            if (ProjektiID.Count > 0)
            {
                foreach (var ProjektID in ProjektiID)
                {
                    SqlHelper.ExecuteNonQuery(cs, "AddProjektDjelatnik", ProjektID, DjelatnikID);
                }
            }
        }
        public static void CreateTim(Tim tim)
        {
            SqlHelper.ExecuteNonQuery(cs, "CreateTim", tim.Naziv, tim.DatumKreiranja, tim.JeAktivan);
        }

        public static void CreateProjekt(Projekt projekt)
        {
            SqlHelper.ExecuteNonQuery(cs, "CreateProjekt", projekt.Naziv, projekt.KlijentID, projekt.DatumOtvaranja, projekt.VoditeljProjektaID, projekt.JeAktivan);
        }

        public static void CreateKlijent(Klijent klijent)
        {
            SqlHelper.ExecuteNonQuery(cs, "CreateKlijent", klijent.Naziv, klijent.Telefon, klijent.Email);
        }

        public static void UpdateDjelatnik(Djelatnik djelatnik, List<int> ProjektiID)
        {
            SqlHelper.ExecuteNonQuery(cs, "UpdateDjelatnik", djelatnik.IDDjelatnik, djelatnik.Ime, djelatnik.Prezime,
                djelatnik.Email, djelatnik.TipDjelatnikaID, djelatnik.TimID);

            if (ProjektiID.Count > 0)
            {
                foreach (var ProjektID in ProjektiID)
                {
                    SqlHelper.ExecuteNonQuery(cs, "AddProjektDjelatnik", ProjektID, djelatnik.IDDjelatnik);
                }
            }
        }

        public static void UpdateTim(Tim tim)
        {
            SqlHelper.ExecuteNonQuery(cs, "UpdateTim", tim.IDTim, tim.Naziv);
        }
        public static void UpdateProjekt(Projekt updProjekt)
        {
            SqlHelper.ExecuteNonQuery(cs, "UpdateProjekt", updProjekt.IDProjekt, updProjekt.Naziv, updProjekt.KlijentID, updProjekt.VoditeljProjektaID);
        }

        public static void UpdateKlijent(Klijent klijent)
        {
            SqlHelper.ExecuteNonQuery(cs, "UpdateKlijent", klijent.IDKlijent, klijent.Naziv, klijent.Telefon, klijent.Email);
        }

        internal static void UpdateTimJeAktivan(int TimID, bool JeAktivan)
        {
            SqlHelper.ExecuteNonQuery(cs, "UpdateTimJeAktivan", TimID, JeAktivan);
        }

        internal static void UpdateKlijentJeAktivan(int KlijentID, bool JeAktivan)
        {
            SqlHelper.ExecuteNonQuery(cs, "UpdateKlijentJeAktivan", KlijentID, JeAktivan);
        }
        internal static void UpdateProjektJeAktivan(int ProjektID, bool JeAktivan)
        {
            SqlHelper.ExecuteNonQuery(cs, "UpdateProjektJeAktivan", ProjektID, JeAktivan);
        }

        public static List<Projekt> GetDjelatnikProjekti(int DjelatnikID)
        {
            List<Projekt> projekti = new List<Projekt>();

            foreach (DataRow row in SqlHelper.ExecuteDataset(cs, "GetDjelatnikProjekti", DjelatnikID).Tables[0].Rows)
            {
                projekti.Add(GetProjektFromDataRow(row));
            }
            return projekti;
        }

        private static Projekt GetProjektFromDataRow(DataRow row)
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

        public static void UpdateZaporka(int DjelatnikID, string zaporka)
        {
            SqlHelper.ExecuteNonQuery(cs, "UpdateZaporka", DjelatnikID, zaporka);
        }

        public static int UpdateDjelatnikJeAktivan(int DjelatnikID, bool JeAktivan)
        {
            int res = SqlHelper.ExecuteNonQuery(cs, "UpdateDjelatnikJeAktivan", DjelatnikID, JeAktivan);
            return res;
        }

        

        private static Klijent GetKlijentFromDataRow(DataRow row)
        {
            Klijent klijent = new Klijent();

            klijent.IDKlijent = int.Parse(row["IDKlijent"].ToString());
            klijent.Naziv = row["Naziv"].ToString();
            klijent.Telefon = row["Telefon"].ToString() == "NULL" ? "" : row["Telefon"].ToString();
            klijent.Email = row["Email"].ToString() == "NULL" ? "" : row["Email"].ToString();
            klijent.JeAktivan = bool.Parse(row["JeAktivan"].ToString());

            return klijent;
        }
    }
}
