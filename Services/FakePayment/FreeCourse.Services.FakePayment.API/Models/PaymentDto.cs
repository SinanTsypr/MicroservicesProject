namespace FreeCourse.Services.FakePayment.API.Models
{
    public class PaymentDto
    {
        public string CardName { get; set; } = null!;
        public string CardNumber { get; set; } = null!;
        public string Expiration { get; set; } = null!;
        public string CVV { get; set; } = null!;
        public decimal TotalPrice { get; set; }
    }
}
