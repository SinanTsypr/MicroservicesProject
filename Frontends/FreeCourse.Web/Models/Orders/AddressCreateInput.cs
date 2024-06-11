namespace FreeCourse.Web.Models.Orders
{
    public class AddressCreateInput
    {
        public string Province { get; set; } = null!;
        public string District { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string Line { get; set; } = null!;
    }
}
