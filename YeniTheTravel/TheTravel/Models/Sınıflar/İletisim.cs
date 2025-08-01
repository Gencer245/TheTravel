using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheTravel.Models.Sınıflar
{
	public class İletisim
	{
        [Key]
        public int Id { get; set; }
        public string Adresimiz { get; set; }
        public string Mail { get; set; }
        public double Numaramız { get; set; }
        public double Enlem { get; set; }
        public double Boylam { get; set; }
        
    }
}