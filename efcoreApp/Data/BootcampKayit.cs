using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data{

    public class BootcampKayit{

        [Key]
        public int KayitID { get; set; }
        public int OgrenciID { get; set; }
        public int BootcampID { get; set; }
        public DateTime KayitTarihi { get; set; }
    }
}