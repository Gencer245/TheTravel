using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheTravel.Models.Sınıflar;
using System.Data.Entity;
namespace TheTravel.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        Context c = new Context();
        [Authorize]
        public ActionResult AnaSayfa()
        {
            return View();

        }
        public ActionResult Index()
        {
            var degerler = c.Blogs.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniBlog()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YeniBlog(Blog p)
        {
            if (ModelState.IsValid)
            {
                c.Blogs.Add(p);
                c.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        public ActionResult BlogSil(int id)
        {
            var b = c.Blogs.Find(id);
            c.Blogs.Remove(b);
            c.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult BlogGetir(int id)
        {
            var bl = c.Blogs.Find(id);
            return View("BlogGetir", bl);

        }
        public ActionResult BlogGuncelle(Blog b)
        {
            var blg = c.Blogs.Find(b.ID);
            blg.Aciklama = b.Aciklama;
            blg.Baslik = b.Baslik;
            blg.BlogImage = b.BlogImage;
            blg.Tarih = b.Tarih;
            c.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult BloglarınDetayları(int id)
        {
            var blg = c.Blogs.Find(id);
            return View(blg);

        }
        public ActionResult YorumListesi()
        {
            var yorumlar = c.Yorumlars.ToList();
            return View(yorumlar);
        }

        public ActionResult YorumSil(int id)
        {
            var b = c.Yorumlars.Find(id);
            c.Yorumlars.Remove(b);
            c.SaveChanges();
            return RedirectToAction("YorumListesi");

        }

        // ----- REHBER KISMI -----

        public ActionResult RehberList()
        {
            var degerler = c.Kıtalar.ToList();
            return View(degerler);
           
        }
        public ActionResult ÜlkelerList(int id)
        {
            
            var ulkeler = c.Ülkeler.Where(u => u.KıtaId == id).ToList();

            if (!ulkeler.Any())
            {
                return HttpNotFound();
            }

            var kitaAdi = c.Kıtalar.Find(id)?.Ad;
            ViewBag.KitaAdi = kitaAdi;

            return View(ulkeler);
        }

        public ActionResult MekanList(int id)
        {
            var ulke = c.Ülkeler.Find(id);
            ViewBag.UlkeAdi = ulke.Ad;  // Ülkenin adını view'a gönderiyoruz
            ViewBag.UlkeId = ulke.Id;   // Ülkenin ID'sini view'a gönderiyoruz
            var mekanlar = c.TuristikMekanlar.Where(m => m.ÜlkeId == id).ToList();  // Bu ülkenin mekanlarını alıyoruz
            return View(mekanlar);
        }

        public ActionResult ÜlkeSil(int id)
        {
            var ülk = c.Ülkeler.Find(id);
            var kıtaId = ülk.KıtaId;
            c.Ülkeler.Remove(ülk);
            c.SaveChanges();
            return RedirectToAction("ÜlkelerList", new { id = kıtaId });

        }

        public ActionResult MekanSil(int id)
        {
            var mkn = c.TuristikMekanlar.Find(id);
            var ülkeId = mkn.ÜlkeId;
            c.TuristikMekanlar.Remove(mkn);
            c.SaveChanges();
            return RedirectToAction("MekanList", new { id = ülkeId });

        }

        public ActionResult MekanlarıGöster(int id)
        {
            var mkn = c.TuristikMekanlar.Find(id);
            return View(mkn);

        }

        public ActionResult MekanGetir(int id)
        {
            var mkn = c.TuristikMekanlar.Find(id);
            return View("MekanGetir", mkn);

        }

        public ActionResult MekanGuncelle(TuristikMekan tm)
        {
            var mkn = c.TuristikMekanlar.Find(tm.Id);
            mkn.Ad = tm.Ad;
            mkn.ResimUrl = tm.ResimUrl;
            mkn.Açıklama = tm.Açıklama;
            c.SaveChanges();
            return RedirectToAction("MekanList", new {id=tm.ÜlkeId});
        }

        [HttpGet]
        public ActionResult YeniÜlke()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniÜlke(Ülke ü)
        {
            c.Ülkeler.Add(ü);
            c.SaveChanges();
            return RedirectToAction("ÜlkelerList", new {id=ü.KıtaId});
        }

        [HttpGet]
        public ActionResult YeniMekan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMekan(TuristikMekan m)
        {
            c.TuristikMekanlar.Add(m);
            c.SaveChanges();
            return RedirectToAction("MekanList", new {id=m.ÜlkeId});
        }

        // Yeni oluşturduğumuz KullaniciListesi metodu
        public ActionResult KullaniciListesi()
        {
            List<Kullanici> kullanicilar = c.Kullanicis.ToList();
            return View(kullanicilar);
        }

        public ActionResult KullaniciGetir(int id)
        {
            var kullanici = c.Kullanicis
                             .Include(x => x.Bloglar)
                             .Include(x => x.Yorumlars)
                             .SingleOrDefault(x => x.ID == id);

            if (kullanici == null)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }

    }

}