using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class DBConnectionServicesExtension
    {
        public static IServiceCollection AddDBConnection(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DatabaseContext>(x => x.UseNpgsql(config.GetConnectionString("Connection")));
            services.AddSingleton<IConnectionMultiplexer>(c => 
            {
                var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis")!);
                return ConnectionMultiplexer.Connect(options);
            });

            return services;
        }
    }
}