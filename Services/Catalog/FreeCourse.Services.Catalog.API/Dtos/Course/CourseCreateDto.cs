using FreeCourse.Services.Catalog.API.Dtos.Category;
using FreeCourse.Services.Catalog.API.Dtos.Feature;

namespace FreeCourse.Services.Catalog.API.Dtos.Course
{
    public class CourseCreateDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string UserId { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public FeatureDto Feature { get; set; }
        public string CategoryId { get; set; } = null!;
    }
}
