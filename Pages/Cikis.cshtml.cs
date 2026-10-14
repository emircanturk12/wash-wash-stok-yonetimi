using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;

namespace Stok_Yonetim.Pages
{
    public class CikisModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            // Sisteme kayýtlý olan kimliði (çerezi) temizle ve kapý dýþarý et
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Giris");
        }
    }
}