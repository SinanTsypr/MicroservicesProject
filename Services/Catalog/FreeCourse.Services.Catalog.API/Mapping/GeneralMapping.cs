using AutoMapper;
using FreeCourse.Services.Catalog.API.Dtos.Category;
using FreeCourse.Services.Catalog.API.Dtos.Course;
using FreeCourse.Services.Catalog.API.Dtos.Feature;
using FreeCourse.Services.Catalog.API.Models;

namespace FreeCourse.Services.Catalog.API.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            //Course Mapping
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Course, CourseUpdateDto>().ReverseMap();

            //Course Mapping
            CreateMap<Category, CategoryDto>().ReverseMap();

            //Feature Mapping
            CreateMap<Feature, FeatureDto>().ReverseMap();
        }
    }
}
