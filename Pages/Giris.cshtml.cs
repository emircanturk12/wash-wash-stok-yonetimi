using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stok_Yonetim.Data;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using System.Threading.Tasks;

namespace Stok_Yonetim.Pages
{
    public class GirisModel : PageModel
    {
        private readonly AppDbContext _context;
        public GirisModel(AppDbContext context) { _context = context; }

        [BindProperty]
        public string KullaniciAdi { get; set; }
        [BindProperty]
        public string Sifre { get; set; }
        public string HataMesaji { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            // Veritabanýnda eþleþen kullanýcýyý ara
            var kullanici = _context.Kullanicilar.FirstOrDefault(k => k.KullaniciAdi == KullaniciAdi && k.Sifre == Sifre);

            if (kullanici == null)
            {
                HataMesaji = "Hatalý Kullanýcý Adý veya Þifre!";
                return Page();
            }

            // --- SÝSTEME GÝRÝÞ BÝLETÝ (CLAIM) HAZIRLANIYOR ---
            var haklar = new List<Claim>
            {
                new Claim(ClaimTypes.Name, kullanici.KullaniciAdi),
                new Claim(ClaimTypes.Role, kullanici.Rol) // 'Yonetici' ya da 'Kullanici' rolü karta basýlýyor
            };

            var kimlik = new ClaimsIdentity(haklar, CookieAuthenticationDefaults.AuthenticationScheme);
            var profil = new ClaimsPrincipal(kimlik);

            // Tarayýcýya güvenli giriþ çerezini býrak
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, profil);

            return RedirectToPage("/Index"); // Doðruysa ana sayfaya uçur
        }
    }
}