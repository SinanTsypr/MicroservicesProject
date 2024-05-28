namespace FreeCourse.Services.Basket.API.Settings
{
    public interface IRedisSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
