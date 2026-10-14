using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stok_Yonetim.Data;
using Stok_Yonetim.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Stok_Yonetim.Pages
{
    [Authorize] // Kilit
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Urun> UrunlerListesi { get; set; } = new List<Urun>();
        public decimal ToplamDepoDegeri { get; set; }
        public int KritikUrunSayisi { get; set; }

        [BindProperty(SupportsGet = true)]
        public string AramaKelimesi { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SeciliKategori { get; set; }

        public void OnGet()
        {
            var sorgu = _context.Urunler.AsQueryable();

            if (!string.IsNullOrEmpty(AramaKelimesi))
            {
                sorgu = sorgu.Where(u => u.Ad.Contains(AramaKelimesi) || u.Kategori.Contains(AramaKelimesi));
            }

            if (!string.IsNullOrEmpty(SeciliKategori))
            {
                sorgu = sorgu.Where(u => u.Kategori == SeciliKategori);
            }

            UrunlerListesi = sorgu.ToList();

            // Veritabaný boþsa hata vermesin diye ufak bir koruma
            if (UrunlerListesi.Count > 0)
            {
                ToplamDepoDegeri = UrunlerListesi.Sum(u => u.Fiyat * u.StokMiktari);
            }
            else
            {
                ToplamDepoDegeri = 0;
            }

            KritikUrunSayisi = _context.Urunler.Count(u => u.StokMiktari < 5);
        }

        public IActionResult OnPostArttir(int id)
        {
            var urun = _context.Urunler.Find(id);
            if (urun != null) { urun.StokMiktari += 1; _context.SaveChanges(); }
            return RedirectToPage(new { AramaKelimesi, SeciliKategori });
        }

        public IActionResult OnPostAzalt(int id)
        {
            var urun = _context.Urunler.Find(id);
            if (urun != null && urun.StokMiktari > 0) { urun.StokMiktari -= 1; _context.SaveChanges(); }
            return RedirectToPage(new { AramaKelimesi, SeciliKategori });
        }

        public IActionResult OnPostSil(int id)
        {
            var urun = _context.Urunler.Find(id);
            if (urun != null) { _context.Urunler.Remove(urun); _context.SaveChanges(); }
            return RedirectToPage(new { AramaKelimesi, SeciliKategori });
        }
        // NORMAL KULLANICI STOK TALEP ETTÝÐÝNDE ÇALIÞACAK KOD
        public IActionResult OnPostStokTalep(string urunAdi)
        {
            var talepEden = User.Identity.Name;

            _context.Bildirimler.Add(new Bildirim
            {
                Mesaj = $"Müþteri '{talepEden}', stokta bitmek üzere olan '{urunAdi}' ürünü için acil stok yenileme talebinde bulundu!",
                Tarih = System.DateTime.Now,
                OkunduMu = false
            });
            _context.SaveChanges();

            // Ekrana yeþil baþarý mesajý yolluyoruz
            TempData["Basari"] = $"'{urunAdi}' için stok talebiniz sistem yöneticisine anýnda iletildi!";
            return RedirectToPage(new { AramaKelimesi, SeciliKategori });
        }

        // YÖNETÝCÝ BÝLDÝRÝMLERÝ SÝLDÝÐÝNDE ÇALIÞACAK KOD
        public IActionResult OnPostBildirimTemizle()
        {
            var okunmamislar = _context.Bildirimler.Where(b => !b.OkunduMu).ToList();
            foreach (var b in okunmamislar)
            {
                b.OkunduMu = true;
            }
            _context.SaveChanges();
            return RedirectToPage(new { AramaKelimesi, SeciliKategori });
        }
    }
}