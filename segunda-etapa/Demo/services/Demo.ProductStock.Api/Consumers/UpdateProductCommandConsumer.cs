using Demo.ProductStock.Api.Contracts.Commands;
using Demo.ProductStock.Api.Infra.Repository;
using Demo.SharedModel.Contracts.Events.Products;
using Demo.SharedModel.Events.Products;
using Demo.SharedModel.Models;
using MassTransit;

namespace Demo.ProductStock.Api.Consumers
{
    public class UpdateProductCommandConsumer : IConsumer<IUpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandConsumer(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task Consume(ConsumeContext<IUpdateProductCommand> context)
        {
            var command = context.Message.Product;
            if (Guid.TryParse(command.ProductId, out var productId))
            {
                var product = await UpdateProduct(productId, command.Quantity);
                await context.Publish<IProductWasUpdatedEvent>(new ProductWasUpdatedEvent(product));
            }
            else
                throw new ArgumentException("ProductId is not valid Guid");
        }

        private async Task<Product> UpdateProduct(Guid productId, int quantity)
        {
            var product = await _productRepository.GetAsync(productId);

            if (product is not null)
            {
                product.QuantityInStock -= quantity;
                await _productRepository.UpdateAsync(product);
            }
            else
                throw new ArgumentException($"Product not found: {productId}");

            return product;
        }
    }
}