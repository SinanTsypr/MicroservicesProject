﻿namespace FreeCourse.Web.Models.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string BuyerId { get; set; } = null!;
        public List<OrderItemViewModel> OrderItems { get; set; } = new();
    }
}
