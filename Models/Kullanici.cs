using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stok_Yonetim.Models
{
    [Table("Kullanicilar")]
    public class Kullanici
    {
        [Key]
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Rol { get; set; }

        // YENİ EKLENEN PROFİL BİLGİLERİ
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }
        public string Uyruk { get; set; }
    }
}