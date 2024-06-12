namespace FreeCourse.Services.FakePayment.API.Models
{
    public class OrderDto
    {
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new();
        public AddressDto Address { get; set; }
    }

    public class AddressDto
    {
        public string Province { get; set; } = null!;
        public string District { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string Line { get; set; } = null!;
    }

    public class OrderItemDto
    {
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
