using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheTravel.Models.Sınıflar;

namespace TheTravel.Controllers
{
    
    public class DefaultController : Controller
    {
        // GET: Default
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Blogs.Take(5).ToList();
            return View(degerler);
        }

        public PartialViewResult Partial1()
        {
            var degerler = c.Blogs.OrderByDescending(x => x.ID).Take(3).ToList();
            return PartialView(degerler);

        }

        public PartialViewResult Partial2()
        {
            var degerler = c.Blogs.ToList();
            return PartialView(degerler);

        }

        public PartialViewResult Partial3()
        {
            var degerler = c.TuristikMekanlar.ToList();
            return PartialView(degerler);
        }

    }
}