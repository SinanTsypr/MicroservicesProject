using AutoMapper;
using FreeCourse.Services.Catalog.API.Dtos.Course;
using FreeCourse.Services.Catalog.API.Models;
using FreeCourse.Services.Catalog.API.Settings;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Messages;
using MassTransit;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.API.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        private readonly IDatabaseSettings _databaseSettings;
        private readonly IPublishEndpoint _publishEndpoint;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings, IPublishEndpoint publishEndpoint)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
            _databaseSettings = databaseSettings;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Shared.Dtos.Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.CategoryId == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            var data = _mapper.Map<List<CourseDto>>(courses);

            return Shared.Dtos.Response<List<CourseDto>>.Success(data, 200);
        }

        public async Task<Shared.Dtos.Response<CourseDto>> GetByIdAsync(string courseId)
        {
            var course = await _courseCollection.Find<Course>(x => x.CourseId == courseId).FirstOrDefaultAsync();

            if (course == null)
            {
                return Shared.Dtos.Response<CourseDto>.Fail("Course not found", 404);
            }

            course.Category = await _categoryCollection.Find<Category>(x => x.CategoryId == course.CategoryId).FirstAsync();

            var data = _mapper.Map<CourseDto>(course);

            return Shared.Dtos.Response<CourseDto>.Success(data, 200);
        }

        public async Task<Shared.Dtos.Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find<Course>(x => x.UserId == userId).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.CategoryId == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            var data = _mapper.Map<List<CourseDto>>(courses);

            return Shared.Dtos.Response<List<CourseDto>>.Success(data, 200);
        }

        public async Task<Shared.Dtos.Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourse = _mapper.Map<Course>(courseCreateDto);

            newCourse.CreateTime = DateTime.Now;

            await _courseCollection.InsertOneAsync(newCourse);

            var data = _mapper.Map<CourseDto>(newCourse);

            return Shared.Dtos.Response<CourseDto>.Success(data, 200);
        }

        public async Task<Shared.Dtos.Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);

            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.CourseId == courseUpdateDto.CourseId, updateCourse);

            if (result == null)
            {
                return Shared.Dtos.Response<NoContent>.Fail("Course not found", 404);
            }

            await _publishEndpoint.Publish<CourseNameChangedEvent>(new CourseNameChangedEvent
            {
                CourseId = courseUpdateDto.CourseId,
                UpdatedName = updateCourse.Name,
            });

            return Shared.Dtos.Response<NoContent>.Success(204);
        }

        public async Task<Shared.Dtos.Response<NoContent>> DeleteAsync(string courseId)
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.CourseId == courseId);

            if (result.DeletedCount > 0)
            {
                return Shared.Dtos.Response<NoContent>.Success(204);
            }
            else
            {
                return Shared.Dtos.Response<NoContent>.Fail("Course not found", 404);
            }
        }
    }
}
