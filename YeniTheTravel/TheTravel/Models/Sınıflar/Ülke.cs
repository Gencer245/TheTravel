using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheTravel.Models.Sınıflar
{
    public class Ülke
    {
        public int Id { get; set; } // Ülkenin benzersiz kimliği
        public string Ad { get; set; } // Ülkenin adı
        public int KıtaId { get; set; } // Ülkenin ait olduğu kıtanın kimliği
        public virtual Kıta Kıta { get; set; } // Kıta ile olan ilişki
        public virtual ICollection<TuristikMekan> TuristikMekanlar { get; set; } // Ülkenin turistik mekanları
    }

}