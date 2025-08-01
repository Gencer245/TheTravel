using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using TheTravel.Models.Sınıflar;

namespace TheTravel.Controllers
{
    public class RehberController : Controller
    {
        private readonly Context _context;

        public RehberController()
        {
            _context = new Context();
        }

        // Ana rehber sayfası - Kıtaları listeler
        public ActionResult Index()
        {
            var kıtalar = _context.Kıtalar.ToList();
            return View(kıtalar);
        }

        // Kıta Detay Sayfası - Kıtaya göre ülkeleri listeler
        public ActionResult KıtaDetay(int kıtaId)
        {
            var kıta = _context.Kıtalar
                .Include(k => k.Ülkeler)
                .FirstOrDefault(k => k.Id == kıtaId);

            if (kıta == null)
            {
                return HttpNotFound();
            }

            return View(kıta);
        }

        // Ülke Detay Sayfası - Ülkeye göre turistik mekanları listeler
        public ActionResult ÜlkeDetay(int ülkeId)
        {
            var ülke = _context.Ülkeler
                .Include(u => u.TuristikMekanlar)
                .FirstOrDefault(u => u.Id == ülkeId);

            if (ülke == null)
            {
                return HttpNotFound();
            }

            return View(ülke);
        }

        // Turistik Mekan Detay Sayfası
        public ActionResult MekanDetay(int mekanId)
        {
            var mekan = _context.TuristikMekanlar
                .FirstOrDefault(m => m.Id == mekanId);

            if (mekan == null)
            {
                return HttpNotFound();
            }

            return View(mekan);
        }
    }
}
