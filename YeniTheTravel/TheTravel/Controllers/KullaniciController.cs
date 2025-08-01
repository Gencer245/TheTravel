using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheTravel.Models.Sınıflar;

namespace TheTravel.Controllers
{
    public class KullaniciController : Controller
    {
        Context c = new Context();
        // Kullanıcı AnaSayfa
        public ActionResult AnaSayfa()
        {
            string kullaniciAdi = Session["Kullanici"] as string;
            if (kullaniciAdi != null)
            {
                var kullanici = c.Kullanicis.FirstOrDefault(x => x.KullaniciAdi == kullaniciAdi);
                if (kullanici != null)
                {
                    ViewBag.KullaniciAdi = kullanici.KullaniciAdi;
                }
            }
            return View();
        }
    }
}