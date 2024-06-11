using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Catalogs
{
    public class CourseUpdateInput
    {
        public string CourseId { get; set; } = null!;
        [Display(Name = "Kurs ismi")]
        public string Name { get; set; } = null!;
        [Display(Name = "Kurs açıklaması")]
        public string Description { get; set; } = null!;
        [Display(Name = "Kurs fiyatı")]
        public decimal Price { get; set; }
        public string? UserId { get; set; } = null!;
        public string? Picture { get; set; } = null!;
        public FeatureViewModel Feature { get; set; }
        [Display(Name = "Kurs kategorisi")]
        public string CategoryId { get; set; } = null!;
        [Display(Name = "Kurs resmi")]
        public IFormFile PhotoFormFile { get; set; }
    }
}
