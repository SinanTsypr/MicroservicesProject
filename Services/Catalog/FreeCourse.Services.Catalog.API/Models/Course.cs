using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FreeCourse.Services.Catalog.API.Models
{
    public class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CourseId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        public string UserId { get; set; } = null!;

        public string Picture { get; set; } = null!;

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreateTime { get; set; }


        // Navigations Property

        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; } = null!;

        [BsonIgnore]
        public Category Category { get; set; } = new();

        public Feature Feature { get; set; } = new();
    }
}
