using System.Text.Json;
using Core.Models;
using Infrastructure.Repository.BasketRepository.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Repository.BasketRepository.Services
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer  redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        {
            var data = await _database.StringGetAsync(basketId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data!);
        }

        public async Task<CustomerBasket> UpdateOrCreateBasketAsync(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(1));

            if (!created) return null!;

            return await GetBasketAsync(basket.Id);
        }
    }
}