using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheTravel.Models.Sınıflar
{
	public class BlogYorum
	{
		public IPagedList<Blog> Deger1 { get; set; }
		public IEnumerable<Yorumlar> Deger2 { get; set; }
		public IEnumerable<Blog> Deger3 { get; set; }

	}
}