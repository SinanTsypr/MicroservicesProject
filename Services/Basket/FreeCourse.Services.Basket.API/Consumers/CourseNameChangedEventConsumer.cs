using FreeCourse.Services.Basket.API.Dtos;
using FreeCourse.Services.Basket.API.Services;
using FreeCourse.Shared.Messages;
using MassTransit;
using System.Text.Json;

namespace FreeCourse.Services.Basket.API.Consumers
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        private readonly RedisService _redisService;

        public CourseNameChangedEventConsumer(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            var keys = _redisService.GetKeys();

            if (keys != null)
            {

                foreach (var key in keys)
                {
                    var basket = await _redisService.GetDb().StringGetAsync(key);

                    var basketDto = JsonSerializer.Deserialize<BasketDto>(basket);

                    basketDto.BasketItems.ForEach(x =>
                    {
                        x.CourseName = x.CourseId == context.Message.CourseId ? context.Message.UpdatedName : x.CourseName;
                    });

                    await _redisService.GetDb().StringSetAsync(key, JsonSerializer.Serialize(basketDto));
                }

            }

        }
    }
}
