using Demo.ProductCatalog.Api.Infra.Cache;
using Demo.ProductCatalog.Api.Infra.Repository;
using Demo.SharedModel.Contracts.Events.Carts;
using Demo.SharedModel.Events.Carts;
using Demo.SharedModel.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Demo.ProductCatalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartCacheRepository _cacheRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public CartController(ICartCacheRepository cacheRepository, IProductRepository productRepository, IPublishEndpoint publishEndpoint)
        {
            _cacheRepository = cacheRepository ?? throw new ArgumentNullException(nameof(cacheRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var cart = await _cacheRepository.GetAsync(id);
            return cart is null ? NotFound() : Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Cart cart)
        {
            await _cacheRepository.InsertAsync(cart);
            return Ok(cart);
        }
        
        [HttpPatch("{id:guid}/item/add")]
        public async Task<IActionResult> AddItemAsync(Guid id, [FromBody] CartItem item)
        {
            var cart = await _cacheRepository.GetAsync(id);
            var product = await _productRepository.GetAsync(item.ProductId);

            if (product is null)
            {
                Console.WriteLine("Product not found");
                return NotFound();
            }

            if (cart is null)
            {
                Console.WriteLine("Cart not found");
                return NotFound();
            }

            if (product.QuantityInStock == 0 || product.QuantityInStock < item.Quantity)
                return BadRequest("Product not enough stock");

            item.UnitPrice = product.SalePrice;
            cart.AddOrUpdate(item);
            await _cacheRepository.UpdateAsync(cart);
            return Ok(cart);
        }

        [HttpPatch("{id:guid}/item/{item}/remove")]
        public async Task<IActionResult> RemoveItemAsync(Guid id, string item)
        {
            var cart = await _cacheRepository.GetAsync(id);
            cart.Remove(item);
            await _cacheRepository.UpdateAsync(cart);
            return Ok(cart);
        }

        [HttpPatch("{id:guid}/checkout")]
        public async Task<IActionResult> CheckoutAsync(Guid id, [FromBody] CreditCard creditCard)
        {
            var cart = await _cacheRepository.GetAsync(id);
            cart.Customer.CreditCard = creditCard;
            cart.WaitPayment();
            await _cacheRepository.UpdateAsync(cart);
            await _publishEndpoint.Publish<ICartWasCheckouted>(new CartWasCheckouted(cart));
            return Accepted(cart);
        }

        [HttpDelete("{id:guid}/cancel")]
        public async Task<IActionResult> CancelAsync(Guid id)
        {
            await _cacheRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}