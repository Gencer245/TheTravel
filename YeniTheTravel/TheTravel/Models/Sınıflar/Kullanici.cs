using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheTravel.Models.Sınıflar
{
    public class Kullanici
    {
        [Key]
        public int ID { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Mail { get; set; }
        public virtual ICollection<Yorumlar> Yorumlars { get; set; }
        public virtual ICollection<Blog> Bloglar { get; set; }
    }
}