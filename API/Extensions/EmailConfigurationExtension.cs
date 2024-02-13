using Core.Models.EmailModels;

namespace API.Extensions
{
    public static class EmailConfigurationExtension
    {
        public static IServiceCollection AddEmailConfiguration(this IServiceCollection services, IConfiguration config)
        {
            var emailConfiguration = config
                .GetSection("EmailConfiguration")
                .Get<EmailConfigurationModel>();
            
            services.AddSingleton(emailConfiguration!);

            return services;
        }
    }
}