using Demo.ProductCatalog.Api.Infra.Repository;
using Demo.ProductCatalog.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.ProductCatalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly IProductRepository _repository;        

        public ProductsController(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));            
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var products = await _repository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var product = await _repository.GetAsync(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Product product)
        {
            await _repository.InsertAsync(product);            
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] string id, [FromBody] Product product)
        {
            //TODO: validar rota com objeto (id)
            await _repository.UpdateAsync(product);            
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id)
        {
            await _repository.DeleteAsync(id.ToString());            
            return Ok();
        }
    }
}