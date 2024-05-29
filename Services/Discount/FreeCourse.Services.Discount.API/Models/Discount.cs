using Dapper.Contrib.Extensions;

namespace FreeCourse.Services.Discount.API.Models
{
    [Table("discount")]
    public class Discount
    {
        public int DiscountId { get; set; }
        public string UserId { get; set; } = null!;
        public int Rate { get; set; }
        public string Code { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
    }
}
