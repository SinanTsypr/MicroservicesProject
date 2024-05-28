namespace FreeCourse.Services.Basket.API.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; } = null!;
        public string DiscountCode { get; set; } = null!;
        public decimal TotalPrice { get => BasketItems.Sum(x => x.Price * x.Quantity); }

        public List<BasketItemDto> BasketItems { get; set; } = new();
    }
}
