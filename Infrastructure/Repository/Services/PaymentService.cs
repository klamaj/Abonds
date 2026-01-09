using Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Infrastructure.Repository.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _config;
        public PaymentService(IConfiguration config)
        {
            _config = config;
        }

        public async Task CreatePaymentIntent()
        {
            StripeConfiguration.ApiKey = _config["StripeConfiguration:SecretKey"];
            var shippingPrice = 0m;

            var service = new PaymentIntentService();
            PaymentIntent intent;
            
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long) 60 * 100 + (long) shippingPrice * 100,
                Currency = "gbp",
                PaymentMethodTypes = new List<string> {"card"}
            };
            intent = await service.CreateAsync(options);
        }
    }
}