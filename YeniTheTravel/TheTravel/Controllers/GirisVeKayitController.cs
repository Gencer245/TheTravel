using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TheTravel.Models.Sınıflar;

namespace TheTravel.Controllers
{
    public class GirisVeKayitController : Controller
    {
        Context c = new Context();

        // GET: GirisYap
        public ActionResult Index()
        {
            return View();
        }

        // Giriş sayfasına git
        public ActionResult Login()
        {
            return View();
        }

        // Kullanıcı girişi işlemi
        [HttpPost]
        public ActionResult Login(Kullanici k)
        {
            // Kullanıcı kontrolü
            var bilgiler = c.Kullanicis.FirstOrDefault(x => x.KullaniciAdi == k.KullaniciAdi && x.Sifre == k.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullaniciAdi, false);
                Session["Kullanici"] = bilgiler.KullaniciAdi;
                return RedirectToAction("Index", "Default"); // Kullanıcıya ait ana sayfaya yönlendir
            }
            else
            {
                // Hatalı giriş durumu
                ViewBag.Hata = "Kullanıcı adı veya şifre yanlış!";
                return View();
            }
        }

        // Admin girişi işlemi
        [HttpPost]
        public ActionResult AdminLogin(Admin ad)
        {
            var bilgiler = c.Admins.FirstOrDefault(x => x.Kullanici == ad.Kullanici && x.Sifre == ad.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Kullanici, false);
                Session["Kullanici"] = bilgiler.Kullanici.ToString();
                return RedirectToAction("AnaSayfa", "Admin"); // Admin paneline yönlendir
            }
            else
            {
                // Hatalı giriş durumu
                ViewBag.Hata = "Admin kullanıcı adı veya şifre yanlış!";
                return View("Login");
            }
        }

        // Çıkış işlemi
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "GirisVeKayit");
        }

        // Kullanıcı Kaydı (Buraya eklendi)
        public ActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KayitOl(Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcı zaten varsa hata mesajı göster
                var mevcutKullanici = c.Kullanicis.FirstOrDefault(x => x.KullaniciAdi == kullanici.KullaniciAdi || x.Mail == kullanici.Mail);
                if (mevcutKullanici != null)
                {
                    ViewBag.Hata = "Bu kullanıcı adı veya e-posta zaten mevcut.";
                    return View();
                }

                // Yeni kullanıcıyı veritabanına ekle
                c.Kullanicis.Add(kullanici);
                c.SaveChanges();
                return RedirectToAction("Login", "GirisVeKayit");
            }
            return View();
        }
    }
}
