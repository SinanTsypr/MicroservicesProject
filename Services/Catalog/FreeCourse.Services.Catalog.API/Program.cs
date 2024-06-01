
using FreeCourse.Services.Catalog.API.Dtos.Category;
using FreeCourse.Services.Catalog.API.Services;
using FreeCourse.Services.Catalog.API.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;

namespace FreeCourse.Services.Catalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));

            //Options Pattern
            builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
            builder.Services.AddSingleton<IDatabaseSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });

            //Dependency Injection Container
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICourseService, CourseService>();

            //JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["IdentityServerURL"];
                options.Audience = "resource_catalog";
                options.RequireHttpsMetadata = false;
            });

            //Her controller için authorize ekledik
            builder.Services.AddControllers(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter());
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var categoryService = serviceProvider.GetRequiredService<ICategoryService>();

                if (!categoryService.GetAllAsync().Result.Data.Any())
                {
                    categoryService.CreateAsync(new CategoryDto { Name = "Asp.net Core Kursu" }).Wait();
                    categoryService.CreateAsync(new CategoryDto { Name = "Asp.net Core API Kursu" }).Wait();
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
