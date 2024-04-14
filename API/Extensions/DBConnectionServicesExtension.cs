using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class DBConnectionServicesExtension
    {
        public static IServiceCollection AddDBConnection(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DatabaseContext>(x => x.UseNpgsql(config.GetConnectionString("Connection")));

            return services;
        }
    }
}