using FreeCourse.Services.Catalog.API.Dtos.Category;
using FreeCourse.Services.Catalog.API.Models;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.API.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}
