using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stok_Yonetim.Data;
using Stok_Yonetim.Models;
// Dosya iþlemleri için gereken 3 yeni kütüphane
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;

namespace Stok_Yonetim.Pages
{
    public class UrunEkleModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment; // Sistemin klasörlerini bulmak için

        public UrunEkleModel(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Urun YeniUrun { get; set; }

        // Bilgisayardan seçilen fotoðraf dosyasýný tutacak deðiþken
        [BindProperty]
        public IFormFile YuklenenFotograf { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // EÐER KULLANICI BÝR FOTOÐRAF SEÇTÝYSE
            if (YuklenenFotograf != null && YuklenenFotograf.Length > 0)
            {
                // 1. Projenin içindeki "wwwroot/images" klasörünün yolunu bul
                var uploadsFolder = Path.Combine(_environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "images");

                // 2. Eðer klasör yoksa otomatik oluþtur
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // 3. Ayný isimli dosyalar karýþmasýn diye ismin baþýna benzersiz bir harf-sayý kodu (Guid) ekle
                var benzersizDosyaAdi = Guid.NewGuid().ToString() + "_" + YuklenenFotograf.FileName;
                var dosyaYolu = Path.Combine(uploadsFolder, benzersizDosyaAdi);

                // 4. Dosyayý fiziksel olarak o klasöre kopyala (Kaydet)
                using (var fileStream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    YuklenenFotograf.CopyTo(fileStream);
                }

                // 5. Veritabanýna ürünün tam adresini yaz
                YeniUrun.Fotograf = "/images/" + benzersizDosyaAdi;
            }

            _context.Urunler.Add(YeniUrun);
            _context.SaveChanges();

            return RedirectToPage("/Index");
        }
    }
}