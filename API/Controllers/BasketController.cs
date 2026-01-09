using Core.Models;
using Infrastructure.Repository.BasketRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateOrCreateBasketAsync(CustomerBasket basket)
        {
            var updatedOrCreatedBasket = await _basketRepository.UpdateOrCreateBasketAsync(basket);
            return Ok(updatedOrCreatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBAsketAsync(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}