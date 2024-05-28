using FreeCourse.Services.Basket.API.Dtos;
using FreeCourse.Shared.Dtos;
using System.Text.Json;

namespace FreeCourse.Services.Basket.API.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> Delete(string userId)
        {
            var status = await _redisService.GetDb().KeyDeleteAsync(userId);

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket not found!", 404);
        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(userId);

            if (String.IsNullOrEmpty(existBasket))
            {
                return Response<BasketDto>.Fail("Basket not found!", 404);
            }

            var response = JsonSerializer.Deserialize<BasketDto>(existBasket);

            return Response<BasketDto>.Success(response!, 200);
        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var response = JsonSerializer.Serialize(basketDto);

            var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, response);

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket could not update or save!", 500);
        }
    }
}
