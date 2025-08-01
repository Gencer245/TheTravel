using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheTravel.Models.Sınıflar
{
    public class Kıta
    {
        public int Id { get; set; } // Kıtanın benzersiz kimliği
        public string Ad { get; set; } // Kıtanın adı
        public virtual ICollection<Ülke> Ülkeler { get; set; } // Kıtanın ülkeleri
    }

}