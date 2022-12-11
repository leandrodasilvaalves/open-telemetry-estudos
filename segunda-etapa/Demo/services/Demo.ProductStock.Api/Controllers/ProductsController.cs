using Demo.ProductStock.Api.Infra.Repository;
using Demo.SharedModel.Events.Products;
using Demo.SharedModel.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Demo.ProductStock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProductsController(IProductRepository repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var products = await _repository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var product = await _repository.GetAsync(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Product product)
        {
            var data = await _repository.InsertAsync(product);
            await _publishEndpoint.Publish<ProductWasIncludedEvent>(new(product));
            return Ok(data);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] Product product)
        {
            var data = await _repository.UpdateAsync(product);
            await _publishEndpoint.Publish<ProductWasUpdatedEvent>(new(product));
            return Ok(data);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var product = await _repository.GetAsync(id);
            await _repository.DeleteAsync(product);
            await _publishEndpoint.Publish<ProductWasExcludedEvent>(new(product));
            return Ok();
        }
    }
}