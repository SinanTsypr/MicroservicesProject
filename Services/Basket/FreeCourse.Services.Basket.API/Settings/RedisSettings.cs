namespace FreeCourse.Services.Basket.API.Settings
{
    public class RedisSettings : IRedisSettings
    {
        public string Host { get; set; } = null!;
        public int Port { get; set; }
    }
}
