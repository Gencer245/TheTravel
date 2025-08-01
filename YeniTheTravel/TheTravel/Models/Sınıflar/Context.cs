using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TheTravel.Models.Sınıflar
{
    public class Context : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Hakkımızda> Hakkımızdas { get; set; }
        public DbSet<İletisim> İletisims { get; set; }
        public DbSet<Yorumlar> Yorumlars { get; set; }

        // Yeni eklenen DbSet'ler
        public DbSet<Kıta> Kıtalar { get; set; }
        public DbSet<Ülke> Ülkeler { get; set; }
        public DbSet<TuristikMekan> TuristikMekanlar { get; set; }
        public DbSet<Kullanici> Kullanicis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Kıta ve Ülke arasındaki ilişki
            modelBuilder.Entity<Ülke>()
                .HasRequired(u => u.Kıta)
                .WithMany(k => k.Ülkeler)
                .HasForeignKey(u => u.KıtaId)
                .WillCascadeOnDelete(true); // Kıta silindiğinde ülke silinsin

            // Ülke ve Turistik Mekan arasındaki ilişki
            modelBuilder.Entity<TuristikMekan>()
                .HasRequired(m => m.Ülke)
                .WithMany(u => u.TuristikMekanlar)
                .HasForeignKey(m => m.ÜlkeId)
                .WillCascadeOnDelete(true); // Ülke silindiğinde turistik mekan silinsin

            // Yorumlar ve Kullanıcı arasındaki ilişki
            modelBuilder.Entity<Yorumlar>()
                .HasRequired(y => y.Kullanici)
                .WithMany(k => k.Yorumlars)
                .HasForeignKey(y => y.KullaniciId)
                .WillCascadeOnDelete(false); // Kullanıcı silindiğinde yorumlar silinsin

            // Yorumlar ve Blog arasındaki ilişki
            modelBuilder.Entity<Yorumlar>()
                .HasRequired(y => y.Blog)
                .WithMany(b => b.Yorumlars)
                .HasForeignKey(y => y.Blogid)
                .WillCascadeOnDelete(true); // Blog silindiğinde yorumlar silinsin

            // Blog ve Kullanıcı arasındaki ilişki (No Cascade)
            modelBuilder.Entity<Blog>()
                .HasRequired(b => b.Kullanici) // Blog'un bir kullanıcıya ait olduğunu belirtir
                .WithMany(k => k.Bloglar) // Kullanıcının birden fazla blogu olabilir
                .HasForeignKey(b => b.KullaniciID)
                .WillCascadeOnDelete(false); // Kullanıcı silindiğinde bloglar silinmesin
        }
    }
}
