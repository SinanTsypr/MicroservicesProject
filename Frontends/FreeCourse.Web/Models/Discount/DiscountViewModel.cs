namespace FreeCourse.Web.Models.Discount
{
    public class DiscountViewModel
    {
        public int Rate { get; set; }
        public string Code { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
    }
}
