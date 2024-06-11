namespace FreeCourse.Web.Models.Orders
{
    public class OrderCreateInput
    {
        public string BuyerId { get; set; }
        public List<OrderItemCreateInput> OrderItems { get; set; } = new();
        public AddressCreateInput Address { get; set; }
    }
}
