using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stok_Yonetim.Models
{
    [Table("Urunler")]
    public class Urun
    {
        [Key]
        public int UrunKod { get; set; }
        public string Ad { get; set; }
        public string Kategori { get; set; }
        public decimal Fiyat { get; set; }
        public int StokMiktari { get; set; }

        // YENİ EKLENEN FOTOĞRAF SÜTUNU (Soru işareti boş bırakılabilir anlamına gelir)
        public string? Fotograf { get; set; }
    }
}