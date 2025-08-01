using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheTravel.Models.Sınıflar
{
	public class Yorumlar
	{
        [Key]
        public int ID { get; set; }
        public string Yorum { get; set; }
        public int KullaniciId { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public int Blogid { get; set; }
        public virtual Blog Blog { get; set; }

    }
}