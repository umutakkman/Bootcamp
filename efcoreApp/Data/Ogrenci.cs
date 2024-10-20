using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data{
    public class Ogrenci{
        
        [Key]
        [Display (Name = "Öğrenci ID")]
        public int OgrenciID { get; set; }

        [Display (Name = "Öğrenci Adı")]
        public string? Ad { get; set; }

        [Display (Name = "Öğrenci Soyadı")]
        public string? Soyad { get; set; }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }
    }
}