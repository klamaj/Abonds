using API.Services.Interfaces;
using API.Services.Services;

namespace API.Extensions
{
    public static class ServicesConfigurationsExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailSender, EmailSender>();
            return services;
        }
    }
}