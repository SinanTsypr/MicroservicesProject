namespace FreeCourse.Services.Basket.API.Dtos
{
    public class BasketDto
    {
        public string? UserId { get; set; } 
        public string? DiscountCode { get; set; }
        public int? DiscountRate { get; set; }
        public decimal TotalPrice { get => BasketItems.Sum(x => x.Price * x.Quantity); }

        public List<BasketItemDto> BasketItems { get; set; } 
    }
}
