namespace FreeCourse.Web.Models.Catalogs
{
    public class CourseViewModel
    {
        public string CourseId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ShortDescription { get => Description.Length > 100 ? Description.Substring(0, 100) + "..." : Description; }
        public decimal Price { get; set; }
        public string? UserId { get; set; } = null!;
        public string? Picture { get; set; } = null!;
        public string? StockPictureUrl { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public FeatureViewModel Feature { get; set; }
        public string CategoryId { get; set; } = null!;
        public CategoryViewModel Category { get; set; }
    }
}
