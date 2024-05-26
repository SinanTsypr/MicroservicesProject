using FreeCourse.Services.Catalog.API.Dtos.Category;
using FreeCourse.Services.Catalog.API.Dtos.Feature;

namespace FreeCourse.Services.Catalog.API.Dtos.Course
{
    public class CourseDto
    {
        public string CourseId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string UserId { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public FeatureDto Feature { get; set; }
        public string CategoryId { get; set; } = null!;
        public CategoryDto Category { get; set; }
    }
}
