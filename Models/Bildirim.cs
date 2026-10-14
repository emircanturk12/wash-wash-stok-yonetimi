using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stok_Yonetim.Models
{
    [Table("Bildirimler")]
    public class Bildirim
    {
        [Key]
        public int Id { get; set; }
        public string Mesaj { get; set; }
        public DateTime Tarih { get; set; }
        public bool OkunduMu { get; set; }
    }
}