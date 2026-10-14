using Microsoft.EntityFrameworkCore;
using Stok_Yonetim.Models;
using System.Collections.Generic;

namespace Stok_Yonetim.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // SQL'deki 'Urunler' tablomuzu C#'taki 'Urun' sınıfına bağlıyoruz
        public DbSet<Bildirim> Bildirimler { get; set; }
        public DbSet<Urun> Urunler { get; set; }
    
public DbSet<Kullanici> Kullanicilar { get; set; }
    }
}
