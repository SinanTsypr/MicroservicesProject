namespace FreeCourse.Web.Models.Orders
{
    public class OrderItemViewModel
    {
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
