using API.Services.Interfaces;
using API.Services.Services;
using Infrastructure.Repository.Interfaces;
using Infrastructure.Repository.Services;

namespace API.Extensions
{
    public static class ServicesConfigurationsExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            return services;
        }
    }
}