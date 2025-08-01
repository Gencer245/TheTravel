using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheTravel.Models.Sınıflar
{
    public class TuristikMekan
    {
        public int Id { get; set; } // Mekanın benzersiz kimliği
        public string Ad { get; set; } // Mekanın adı
        public string Açıklama { get; set; } // Mekanın açıklaması
        public string ResimUrl { get; set; } // Mekanın resmi için URL
        public int ÜlkeId { get; set; } // Mekanın ait olduğu ülkenin kimliği
        public virtual Ülke Ülke { get; set; } // Ülke ile olan ilişki
    }

}