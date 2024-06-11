using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Orders
{
    public class CheckoutInfoInput
    {
        [Display(Name = "İl")]
        public string Province { get; set; } = null!;
        [Display(Name = "İlçe")]
        public string District { get; set; } = null!;
        [Display(Name = "Cadde")]
        public string Street { get; set; } = null!;
        [Display(Name = "Posta Kodu")]
        public string ZipCode { get; set; } = null!;
        [Display(Name = "Adres")]
        public string Line { get; set; } = null!;


        [Display(Name = "Kart İsim Soyisim")]
        public string CardName { get; set; } = null!;
        [Display(Name = "Kart Numarası")]
        public string CardNumber { get; set; } = null!;
        [Display(Name = "Son Kullanma Tarihi(Ay/Yıl)")]
        public string Expiration { get; set; } = null!;
        [Display(Name = "CVV/CVC2")]
        public string CVV { get; set; } = null!;
        public decimal TotalPrice { get; set; }
    }
}
