using System;
using System.Linq;
using System.Web.Mvc;
using TheTravel.Models.Sınıflar;

namespace TheTravel.Controllers
{
    public class ProfilController : Controller
    {
        Context c = new Context();

        // GET: Profil
        public ActionResult Index()
        {
            string kullaniciAdi = Session["Kullanici"] as string;

            if (string.IsNullOrEmpty(kullaniciAdi))
            {
                return RedirectToAction("Giris", "Account");
            }

            var kullanici = c.Kullanicis.SingleOrDefault(k => k.KullaniciAdi == kullaniciAdi);

            if (kullanici == null)
            {
                return HttpNotFound();
            }

            return View(kullanici);
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
            string kullaniciAdi = Session["Kullanici"] as string;
            if (string.IsNullOrEmpty(kullaniciAdi))
            {
                ModelState.AddModelError("", "Kullanıcı oturumu açık değil. Lütfen giriş yapın.");
                return View(p);
            }

            var kullanici = c.Kullanicis.SingleOrDefault(k => k.KullaniciAdi == kullaniciAdi);
            if (kullanici == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View(p);
            }

            ModelState.Remove("KullaniciID");
            p.KullaniciID = kullanici.ID;

            if (ModelState.IsValid)
            {
                try
                {
                    c.Blogs.Add(p);
                    c.SaveChanges();
                    return RedirectToAction("Bloglarim");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Bir hata oluştu: " + ex.Message);
                }
            }
            else
            {
                var errorList = string.Join("; ", ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage));
                ModelState.AddModelError("", "Lütfen aşağıdaki hataları düzeltin: " + errorList);
            }

            return View(p);
        }

        // Bloglarım metodu
        public ActionResult Bloglarim()
        {
            string kullaniciAdi = Session["Kullanici"] as string;

            if (string.IsNullOrEmpty(kullaniciAdi))
            {
                return RedirectToAction("Index", "Profil");
            }

            var kullanici = c.Kullanicis.SingleOrDefault(k => k.KullaniciAdi == kullaniciAdi);

            if (kullanici == null)
            {
                return HttpNotFound();
            }

            var bloglar = c.Blogs.Where(b => b.KullaniciID == kullanici.ID).ToList();

            return View(bloglar);
        }

        // Blog silme metodu
        public ActionResult BlogSil(int id)
        {
            var blog = c.Blogs.Find(id);
            if (blog != null)
            {
                c.Blogs.Remove(blog);
                c.SaveChanges();
            }
            return RedirectToAction("Bloglarim");
        }

        // Blog detayları metodu
        public ActionResult BloglarınDetayları(int id)
        {
            var blog = c.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // Blog güncelleme metodu (GET)
        [HttpGet]
        public ActionResult BlogGetir(int id)
        {
            var blog = c.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // Blog güncelleme metodu (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlogGuncelle(Blog b)
        {
            var blog = c.Blogs.Find(b.ID);
            if (blog == null)
            {
                return HttpNotFound();
            }

            blog.Baslik = b.Baslik;
            blog.Tarih = b.Tarih;
            blog.BlogImage = b.BlogImage;
            blog.Aciklama = b.Aciklama;
            c.SaveChanges();

            return RedirectToAction("Bloglarim");
        }

        // Yorumlarım metodu
        public ActionResult Yorumlarim()
        {
            string kullaniciAdi = Session["Kullanici"] as string;

            if (string.IsNullOrEmpty(kullaniciAdi))
            {
                return RedirectToAction("Index", "Profil");
            }

            var kullanici = c.Kullanicis.SingleOrDefault(k => k.KullaniciAdi == kullaniciAdi);

            if (kullanici == null)
            {
                return HttpNotFound();
            }

            var yorumlar = c.Yorumlars.Where(y => y.KullaniciId == kullanici.ID).ToList();

            return View(yorumlar);
        }

        // Yorum silme metodu
        public ActionResult YorumSil(int id)
        {
            var yorum = c.Yorumlars.Find(id);
            if (yorum != null)
            {
                c.Yorumlars.Remove(yorum);
                c.SaveChanges();
            }
            return RedirectToAction("Yorumlarim");
        }

        // Yorum detayları metodu
        public ActionResult YorumDetayları(int id)
        {
            var yorum = c.Yorumlars.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

        // Yorum güncelleme metodu (GET)
        [HttpGet]
        public ActionResult YorumGetir(int id)
        {
            var yorum = c.Yorumlars.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

        // Yorum güncelleme metodu (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YorumGuncelle(Yorumlar y)
        {
            var yorum = c.Yorumlars.Find(y.ID);
            if (yorum == null)
            {
                return HttpNotFound();
            }

            yorum.Yorum = y.Yorum; // Yorum içeriğini güncelle
            // Gerekirse diğer alanları da güncelleyebilirsiniz
            c.SaveChanges();

            return RedirectToAction("Yorumlarim");
        }
    }
}