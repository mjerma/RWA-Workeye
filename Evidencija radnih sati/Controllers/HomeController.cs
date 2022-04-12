using Evidencija_radnih_sati.Models;
using Evidencija_radnih_sati.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Evidencija_radnih_sati.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Prijava()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Prijava(PrijavaVM model)
        {
            Djelatnik djelatnik = Repo.PrijavaKorisnika(model.Email, model.Zaporka);

            if (djelatnik != null)
            {
                Session["korisnik"] = djelatnik;
                Session["direktorTipDjelatnikaID"] = 1;
                Session["voditeljTipDjelatnikaID"] = 2;
                return RedirectToAction("PracenjeRada", new { datum = DateTime.Now });
            }

            else 
            {
                model.PrijavaError = "Neispravan email ili zaporka";
                return View("Prijava", model);
            } 

        }

        public ActionResult PracenjeRada(DateTime datum)
        {
            if (Session["korisnik"] != null)
            {
                Djelatnik korisnik = Session["korisnik"] as Djelatnik;
                Satnica satnica = Repo.GetDjelatnikSatnica(datum, korisnik.IDDjelatnik);
                List<Projekt> projekti = Repo.GetDjelatnikProjekti(korisnik.IDDjelatnik);

                //U slucaju da ima kreirane satnice na poslani datum
                if (satnica != null)
                {
                    List<ProjektSatnica> projektiSatnice = Repo.GetProjektiSatnice(satnica.IDSatnica);

                    int brojProjekata = projektiSatnice.Count;

                    string[] zabiljezeno = new string[brojProjekata];
                    int[] radni = new int[brojProjekata];
                    int[] prekovremeni = new int[brojProjekata];

                    for (int i = 0; i < brojProjekata; i++)
                    {
                        zabiljezeno[i] = projektiSatnice[i].Zabiljezeno;
                        radni[i] = projektiSatnice[i].RadniSati;
                        prekovremeni[i] = projektiSatnice[i].PrekovremeniSati;
                    }

                    var model = new PracenjeRadaVM
                    {
                        IDDjelatnik = korisnik.IDDjelatnik,
                        Satnica = satnica,
                        Projekti = projekti,
                        Datum = datum,
                        Zabiljezeno = zabiljezeno,
                        radniSati = radni,
                        prekovremeniSati = prekovremeni,
                        zabiljezenoUkupno = satnica.ZabiljezenoUkupno,
                        radniSatiUkupno = satnica.RadniSatiUkupno,
                        prekovremeniSatiUkupno = satnica.PrekovremeniSatiUkupno,
                        Komentar = satnica.Komentar
                    };

                    return View(model);
                }

                //U slucaju da nema kreirane satnice na poslani datum
                else
                {
                    var model = new PracenjeRadaVM
                    {
                        IDDjelatnik = korisnik.IDDjelatnik,
                        Projekti = projekti,
                        Datum = datum
                    };
                    return View(model);
                }
            }

            else return RedirectToAction("Prijava");
        }

        [HttpPost]
        public ActionResult PracenjeRada(PracenjeRadaVM model)
        {
            if (model.PromjenaDatuma)
            {
                return RedirectToAction("PracenjeRada", new { datum = model.Datum });
            }

            List<Projekt> projekti = Repo.GetDjelatnikProjekti(model.IDDjelatnik);
            Djelatnik djelatnik = Repo.GetDjelatnik(model.IDDjelatnik);
            Djelatnik voditelj = Repo.GetVoditeljTima(djelatnik.TimID.Value);

            Satnica satnica = new Satnica
            {
                DatumSatnice = model.Datum,
                DatumSlanja = DateTime.Now,
                ZabiljezenoUkupno = model.zabiljezenoUkupno,
                RadniSatiUkupno = model.radniSatiUkupno,
                PrekovremeniSatiUkupno = model.prekovremeniSatiUkupno,
                Komentar = model.Komentar,
                DjelatnikID = model.IDDjelatnik,
                VoditeljID = voditelj.IDDjelatnik,
                JePoslano = true
            };

            bool updated = false;

            if (satnica.VoditeljID == satnica.DjelatnikID)
            {
                satnica.VoditeljID = Repo.GetDirektor().IDDjelatnik;
            }

            // Ako se mijenjala već postojeća satnica, napravi update
            if (model.IDSatnica != 0)
            {
                satnica.IDSatnica = model.IDSatnica;
                Repo.UpdateSatnica(satnica);
                updated = true;
            }

            else
            {
                satnica.IDSatnica = Repo.CreateSatnica(satnica);
            }

            for (int i = 0; i < projekti.Count; i++)
            {
                ProjektSatnica projektSatnica = new ProjektSatnica
                {
                    Zabiljezeno = model.Zabiljezeno[i],
                    RadniSati = model.radniSati[i],
                    PrekovremeniSati = model.prekovremeniSati[i],
                    ProjektID = projekti[i].IDProjekt,
                    SatnicaID = satnica.IDSatnica
                };

                if (updated)
                {
                    Repo.UpdateProjektSatnica(projektSatnica);
                }
                else
                {
                    Repo.CreateProjektSatnica(projektSatnica);
                }
            }
            return RedirectToAction("PracenjeRada", new { datum = model.Datum });
        }

        public ActionResult PotvrdaSatnica()
        {
            Djelatnik korisnik = Session["korisnik"] as Djelatnik;
            int direktorTipDjelatnikaID = Convert.ToInt32(Session["direktorTipDjelatnikaID"]);
            int voditeljTipDjelatnikaID = Convert.ToInt32(Session["voditeljTipDjelatnikaID"]);

            if (korisnik != null)
            {
                if (korisnik.TipDjelatnikaID == direktorTipDjelatnikaID || korisnik.TipDjelatnikaID == voditeljTipDjelatnikaID)
                {
                    List<Satnica> satnice = Repo.GetSatniceZaPotvrdu(korisnik.IDDjelatnik);
                    List<Djelatnik> djelatnici = Repo.GetDjelatnici();

                    // Spoji satnice i djelatnike u view model
                    var model = from s in satnice
                                join d in djelatnici on s.DjelatnikID equals d.IDDjelatnik
                                into table1
                                from d in table1
                                select new PotvrdaSatniceVM { satnica = s, djelatnik = d };

                    return View(model);
                }
                else return RedirectToAction("PracenjeRada", new { datum = DateTime.Now });
            }
            else return RedirectToAction("Prijava");
        }

        public ActionResult DetaljiSatnice(int SatnicaID)
        {
            Djelatnik korisnik = Session["korisnik"] as Djelatnik;
            int direktorTipDjelatnikaID = Convert.ToInt32(Session["direktorTipDjelatnikaID"]);
            int voditeljTipDjelatnikaID = Convert.ToInt32(Session["voditeljTipDjelatnikaID"]);

            if (korisnik != null)
            {
                if (korisnik.TipDjelatnikaID == direktorTipDjelatnikaID || korisnik.TipDjelatnikaID == voditeljTipDjelatnikaID)
                {
                    Satnica satnica = Repo.GetSatnica(SatnicaID);
                    List<ProjektSatnica> projektiSatnice = Repo.GetProjektiSatnice(satnica.IDSatnica);
                    List<Projekt> projekti = Repo.GetDjelatnikProjekti(satnica.DjelatnikID);
                    Djelatnik djelatnik = Repo.GetDjelatnik(satnica.DjelatnikID);

                    int brojProjekata = projektiSatnice.Count;

                    string[] zabiljezeno = new string[brojProjekata];
                    int[] radni = new int[brojProjekata];
                    int[] prekovremeni = new int[brojProjekata];

                    for (int i = 0; i < brojProjekata; i++)
                    {
                        zabiljezeno[i] = projektiSatnice[i].Zabiljezeno;
                        radni[i] = projektiSatnice[i].RadniSati;
                        prekovremeni[i] = projektiSatnice[i].PrekovremeniSati;
                    }

                    var model = new PracenjeRadaVM
                    {
                        IDDjelatnik = satnica.DjelatnikID,
                        DjelatnikPunoIme = djelatnik.PunoIme,
                        Satnica = satnica,
                        Projekti = projekti,
                        Datum = satnica.DatumSatnice,
                        Zabiljezeno = zabiljezeno,
                        radniSati = radni,
                        prekovremeniSati = prekovremeni,
                        zabiljezenoUkupno = satnica.ZabiljezenoUkupno,
                        radniSatiUkupno = satnica.RadniSatiUkupno,
                        prekovremeniSatiUkupno = satnica.PrekovremeniSatiUkupno,
                        Komentar = satnica.Komentar
                    };
                    return View(model);
                }
                else return RedirectToAction("PracenjeRada", new { datum = DateTime.Now });
                
            }
            else return RedirectToAction("Prijava");
        }

        [HttpPost]
        public ActionResult DetaljiSatnice(PracenjeRadaVM model, string button) 
        {
            switch (button)
            {
                case "Vrati na doradu":
                    Repo.UpdateSatnicaJePoslano(model.IDSatnica, model.Komentar);
                    break;
                case "Ažuriraj":
                    Repo.SpremiSatnicu(model.IDSatnica);
                    break;
                case "Odustani":
                    return RedirectToAction("PotvrdaSatnica");
            }

            return RedirectToAction("PotvrdaSatnica");
        }

        public ActionResult Navigacija()
        {
            Djelatnik korisnik = Session["korisnik"] as Djelatnik;
            int direktorTipDjelatnikaID = Convert.ToInt32(Session["direktorTipDjelatnikaID"]);
            int voditeljTipDjelatnikaID = Convert.ToInt32(Session["voditeljTipDjelatnikaID"]);

            if (korisnik.TipDjelatnikaID == direktorTipDjelatnikaID || korisnik.TipDjelatnikaID == voditeljTipDjelatnikaID)
            {
                return PartialView("NavigacijaVoditelji");
            }

            else return PartialView("NavigacijaDjelatnici");
        }

        [HttpGet]
        public ActionResult Profil()
        {
            if (Session["korisnik"] != null)
            {
                Djelatnik djelatnik = Session["korisnik"] as Djelatnik;
                TipDjelatnika tipDjelatnika = Repo.GetTipDjelatnika(djelatnik.TipDjelatnikaID);

                ProfilVM model = new ProfilVM 
                { 
                    djelatnik = djelatnik,
                    TipDjelatnika = tipDjelatnika.Naziv
                };

                return View(model);
            }

            else return RedirectToAction("Prijava");
        }

        [HttpPost]
        public ActionResult Profil(int IDDjelatnik, string novaZaporka) 
        {
            if (novaZaporka != "")
            {
                Repo.UpdateZaporka(IDDjelatnik, novaZaporka);
            }

            return RedirectToAction("Profil");
        }

        public ActionResult PromjenaJezika(string jezik)
        {
            if (jezik != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(jezik);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(jezik);
                var kuki = new HttpCookie("culture");
                kuki.Value = jezik;
                Response.Cookies.Add(kuki);
            }
            return RedirectToAction("PracenjeRada", new { datum = DateTime.Now });
        }
    }
}