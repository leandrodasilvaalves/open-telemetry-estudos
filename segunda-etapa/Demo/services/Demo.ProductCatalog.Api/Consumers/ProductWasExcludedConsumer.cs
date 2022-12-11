using System.Text.Json;
using Demo.ProductCatalog.Api.Infra.Repository;
using Demo.SharedModel.Contracts.Events.Products;
using MassTransit;

namespace Demo.ProductCatalog.Api.Consumers
{
    public class ProductWasExcludedConsumer : IConsumer<IProductWasExcludedEvent>
    {
        private readonly IProductRepository _repository;

        public ProductWasExcludedConsumer(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Consume(ConsumeContext<IProductWasExcludedEvent> context)
        {
            var product = context.Message?.Data;
            Console.WriteLine("Received Message: {0}", JsonSerializer.Serialize(product, new JsonSerializerOptions { WriteIndented = true }));
            await _repository.DeleteAsync(product.Id.ToString());
        }
    }
}