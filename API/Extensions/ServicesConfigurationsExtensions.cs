using API.Services.Interfaces;
using API.Services.Services;
using Infrastructure.Data.GoogleService.Interfaces;
using Infrastructure.Data.GoogleService.Services;
using Infrastructure.Repository.BasketRepository.Interfaces;
using Infrastructure.Repository.BasketRepository.Services;
using Infrastructure.Repository.ClientsRepository.Interfaces;
using Infrastructure.Repository.ClientsRepository.Services;
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
            services.AddScoped<IInterestsRepo, InterestsRepo>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped<IGoogleService, GoogleService>();
            return services;
        }
    }
}