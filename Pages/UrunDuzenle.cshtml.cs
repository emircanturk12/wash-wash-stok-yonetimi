using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stok_Yonetim.Data;
using Stok_Yonetim.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Linq;

namespace Stok_Yonetim.Pages
{
    public class UrunDuzenleModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public UrunDuzenleModel(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // Formdan gelecek ve forma gönderilecek ürün verisi
        [BindProperty]
        public Urun DuzenlenenUrun { get; set; }

        // Eðer kullanýcý fotoðrafý deðiþtirmek isterse buraya düþecek
        [BindProperty]
        public IFormFile YeniFotograf { get; set; }

        // SAYFA ÝLK AÇILDIÐINDA ÇALIÞACAK KOD (Ürünü bul ve kutulara doldur)
        public IActionResult OnGet(int id)
        {
            DuzenlenenUrun = _context.Urunler.FirstOrDefault(u => u.UrunKod == id);

            // Eðer ürün bulunamazsa ana sayfaya at
            if (DuzenlenenUrun == null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        // KAYDET BUTONUNA BASILDIÐINDA ÇALIÞACAK KOD
        public IActionResult OnPost()
        {
            // Orijinal ürünü veritabanýndan çaðýr
            var mevcutUrun = _context.Urunler.FirstOrDefault(u => u.UrunKod == DuzenlenenUrun.UrunKod);
            if (mevcutUrun == null) { return RedirectToPage("/Index"); }

            // Kutulardaki yeni metinleri eski ürüne aktar (Güncelle)
            mevcutUrun.Ad = DuzenlenenUrun.Ad;
            mevcutUrun.Kategori = DuzenlenenUrun.Kategori;
            mevcutUrun.Fiyat = DuzenlenenUrun.Fiyat;
            mevcutUrun.StokMiktari = DuzenlenenUrun.StokMiktari;

            // EÐER KULLANICI "YENÝ BÝR FOTOÐRAF" SEÇTÝYSE
            if (YeniFotograf != null && YeniFotograf.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "images");
                if (!Directory.Exists(uploadsFolder)) { Directory.CreateDirectory(uploadsFolder); }

                var benzersizDosyaAdi = Guid.NewGuid().ToString() + "_" + YeniFotograf.FileName;
                var dosyaYolu = Path.Combine(uploadsFolder, benzersizDosyaAdi);

                using (var fileStream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    YeniFotograf.CopyTo(fileStream);
                }

                // Sadece yeni fotoðraf yüklendiyse veritabanýndaki yolu deðiþtir
                mevcutUrun.Fotograf = "/images/" + benzersizDosyaAdi;
            }

            // Deðiþiklikleri mühürle ve ana sayfaya dön
            _context.SaveChanges();
            return RedirectToPage("/Index");
        }
    }
}