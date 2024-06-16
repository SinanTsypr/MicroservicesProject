using FreeCourse.Gateway.DelegateHandlers;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace FreeCourse.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Services
            builder.Services.AddHttpClient<TokenExchangeDelegateHandler>();

            //JWT
            builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", options =>
            {
                options.Authority = builder.Configuration["IdentityServerURL"];
                options.Audience = "resource_gateway";
                options.RequireHttpsMetadata = false;
            });

            // Ocelot
            builder.Services.AddOcelot().AddDelegatingHandler<TokenExchangeDelegateHandler>();

            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddJsonFile($"configuration.{builder.Environment.EnvironmentName}.json")
                .AddEnvironmentVariables();

            var app = builder.Build();

            // Ocelot Middleware
            app.UseOcelot();

            app.Run();
        }
    }
}
