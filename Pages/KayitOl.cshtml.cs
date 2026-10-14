using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stok_Yonetim.Data;
using Stok_Yonetim.Models;
using System.Linq;

namespace Stok_Yonetim.Pages
{
    public class KayitOlModel : PageModel
    {
        private readonly AppDbContext _context;
        public KayitOlModel(AppDbContext context) { _context = context; }

        [BindProperty]
        public Kullanici YeniKullanici { get; set; }
        public string HataMesaji { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            // Kullanýcý adý daha önce alýnmýþ mý kontrol et
            var varMi = _context.Kullanicilar.Any(k => k.KullaniciAdi == YeniKullanici.KullaniciAdi);
            if (varMi)
            {
                HataMesaji = "Bu kullanýcý adý zaten alýnmýþ!";
                return Page();
            }

            // Yeni kaydolan herkes varsayýlan olarak 'Kullanici' rolünde baþlar (Admin olamazlar)
            YeniKullanici.Rol = "Kullanici";

            _context.Kullanicilar.Add(YeniKullanici);
            _context.SaveChanges();

            return RedirectToPage("/Giris"); // Kayýt bitince giriþe gönder
        }
    }
}