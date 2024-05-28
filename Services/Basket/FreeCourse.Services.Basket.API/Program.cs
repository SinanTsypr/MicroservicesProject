
using FreeCourse.Services.Basket.API.Services;
using FreeCourse.Services.Basket.API.Settings;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;

namespace FreeCourse.Services.Basket.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Options Pattern
            builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));

            //Redis
            builder.Services.AddSingleton<RedisService>(sp =>
            {
                var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
                var redis = new RedisService(redisSettings.Host, redisSettings.Port);
                redis.Connect();
                return redis;
            });

            //Policy
            var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            //Mapping
            JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["IdentityServerURL"];
                options.Audience = "resource_basket";
                options.RequireHttpsMetadata = false;
            });

            //Services
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            builder.Services.AddScoped<IBasketService, BasketService>();

            //Yetkili kullanýcý için bütün controllera authorize policy eklendi
            builder.Services.AddControllers(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
