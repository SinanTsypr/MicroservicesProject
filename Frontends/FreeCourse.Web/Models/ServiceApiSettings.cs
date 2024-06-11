namespace FreeCourse.Web.Models
{
    public class ServiceApiSettings
    {
        public string IdentityBaseUri { get; set; } = null!;
        public string GatewayBaseUri { get; set; } = null!;
        public string PhotoStockUri { get; set; } = null!;
        public ServiceApi Catalog { get; set; }
        public ServiceApi PhotoStock { get; set; }
        public ServiceApi Basket { get; set; }
        public ServiceApi Discount { get; set; }
        public ServiceApi Payment { get; set; }
        public ServiceApi Order { get; set; }
    }

    public class ServiceApi {
        public string Path { get; set; }
    }
}
