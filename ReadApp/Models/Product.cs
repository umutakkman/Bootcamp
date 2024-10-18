using System.ComponentModel.DataAnnotations;

namespace ReadApp.Models
{

    public class Product
    {
        public int ProductID { get; set; }
        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage = "Ürün adını giriniz.")]
        [StringLength(150, ErrorMessage = "Ürün adı en fazla 150 karakter olmalıdır.")]
        [MinLength(5, ErrorMessage = "Ürün adı en az 5 karakter olmalıdır.")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Fiyat")]
        [Required(ErrorMessage = "Ürün fiyatını giriniz.")]
        [Range(0, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır.")]
        public decimal? Price { get; set; }

        [Display(Name = "Görsel")]
        public string? Image { get; set; } = string.Empty;

        [Display(Name = "Stok Adedi")]
        [Required(ErrorMessage = "Ürün stok adetini giriniz.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stok adedi 0'dan büyük olmalıdır.")]
        public int? Stock { get; set; }

        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Ürün kategorisini giriniz.")]
        public int? CategoryID { get; set; }
    }
}