using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheTravel.Models.Sınıflar;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace TheTravel.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        Context c = new Context();
        BlogYorum by = new BlogYorum(); //*
        public ActionResult Index(int sayfa = 1)
        {
            by.Deger1 = c.Blogs.OrderByDescending(x => x.ID).ToPagedList(sayfa, 5);
            by.Deger3 = c.Blogs.OrderByDescending(x => x.ID).Take(3).ToList();
            return View(by);
        }
        public ActionResult BlogDetay(int id, int sayfa = 1)
        {
            by.Deger1 = c.Blogs.Where(x => x.ID == id)
                               .OrderBy(x => x.ID)
                               .ToPagedList(sayfa, 1);

            by.Deger2 = c.Yorumlars
                         .Include("Kullanici") // EKLEDİĞİMİZ KISIM BURASI
                         .Where(x => x.Blogid == id)
                         .ToList();

            ViewBag.BlogId = id;
            return View(by);
        }
        [HttpGet]
        public PartialViewResult YorumYap(int id)
        {
            ViewBag.deger = id;
            return PartialView();
        }

        [HttpPost]
        public ActionResult YorumYap(Yorumlar y)
        {
            var kullaniciAdi = Session["Kullanici"] as string;
            if (string.IsNullOrEmpty(kullaniciAdi))
            {
                return Json(new { success = false, message = "Kullanıcı bilgisi alınamadı. Lütfen giriş yapınız." });
            }

            var kullanici = c.Kullanicis.FirstOrDefault(x => x.KullaniciAdi == kullaniciAdi);
            if (kullanici == null)
            {
                return Json(new { success = false, message = "Kullanıcı bilgisi geçerli değil." });
            }

            y.KullaniciId = kullanici.ID; // Kullanıcı ID'sini al
            c.Yorumlars.Add(y);
            c.SaveChanges();

            return Json(new { success = true });
        }


    }
}




