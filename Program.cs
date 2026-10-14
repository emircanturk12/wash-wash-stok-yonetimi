using Microsoft.EntityFrameworkCore;
using Stok_Yonetim.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=DESKTOP-4RONJM4\\SQLEXPRESS02;Database=StokYonetimDB;Trusted_Connection=True;TrustServerCertificate=True;"));

// ÇEREZ VE GÜVENLÝK AYARLARI
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Giris";
        options.AccessDeniedPath = "/Giris";
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// BU ÝKÝSÝNÝN SIRASI KESÝNLÝKLE BÖYLE OLMALIDIR
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();  