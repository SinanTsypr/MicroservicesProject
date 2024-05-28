namespace FreeCourse.Services.Catalog.API.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string CourseCollectionName { get; set; } = null!;
        public string CategoryCollectionName { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }
}
