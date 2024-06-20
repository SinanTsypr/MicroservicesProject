
using FreeCourse.Services.Order.Infrastructure;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Reflection;
using MassTransit;
using FreeCourse.Services.Order.Application.Consumers;

namespace FreeCourse.Services.Order.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Masstransit - RabbitMQ(port: 5672, management-port:15672)
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateOrderMessageCommandConsumer>();
                x.AddConsumer<CourseNameChangedEventConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    cfg.ReceiveEndpoint("create-order-service", e =>
                    {
                        e.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("course-name-changed-event-order-service", e =>
                    {
                        e.ConfigureConsumer<CourseNameChangedEventConsumer>(context);
                    });
                });
            });
            //Masstransit v8 sonrasý için ayrý olarak çaðýrmaya gerek kalmadý
            //https://github.com/MassTransit/MassTransit/discussions/3051
            //https://code-maze.com/masstransit-rabbitmq-aspnetcore/
            //builder.Services.AddMassTransitHostedService();

            builder.Services.AddDbContext<OrderDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), configure =>
                {
                    configure.MigrationsAssembly("FreeCourse.Services.Order.Infrastructure");
                });
            });

            //Services
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

            //Policy
            var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            //Mapping
            JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["IdentityServerURL"];
                options.Audience = "resource_order";
                options.RequireHttpsMetadata = false;
            });

            // Mediatr
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(FreeCourse.Services.Order.Application.Handlers.CreateOrderCommandHandler).Assembly));

            //Yetkili kullanýcý için bütün controllera authorize policy eklendi
            builder.Services.AddControllers(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            //Otomatik update-database
            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var orderDbContext = serviceProvider.GetRequiredService<OrderDbContext>();
                orderDbContext.Database.Migrate();
            }

            app.MapControllers();

            app.Run();
        }
    }
}
