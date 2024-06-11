namespace FreeCourse.Web.Models.FakePayments
{
    public class PaymentInfoInput
    {
        public string CardName { get; set; } = null!;
        public string CardNumber { get; set; } = null!;
        public string Expiration { get; set; } = null!;
        public string CVV { get; set; } = null!;
        public decimal TotalPrice { get; set; }

    }
}
