using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheTravel.Models.Sınıflar;


namespace TheTravel.Controllers
{
    public class İletisimController : Controller
    {
        // GET: İletisim
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.İletisims.ToList();
            return View(degerler);
        }
    }
}