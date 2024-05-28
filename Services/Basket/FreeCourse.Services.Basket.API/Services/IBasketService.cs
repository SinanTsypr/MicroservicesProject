using FreeCourse.Services.Basket.API.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Basket.API.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>> Delete(string userId);
    }
}
