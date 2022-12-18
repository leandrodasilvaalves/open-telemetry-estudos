using Demo.ProductCatalog.Api.Infra.Repository;
using Demo.ProductCatalog.Api.Models;
using Demo.SharedModel.Contracts.Events.Products;
using MassTransit;

namespace Demo.ProductCatalog.Api.Consumers
{
    public class ProductWasIncludedConsumer : IConsumer<IProductWasIncludedEvent>
    {
        private readonly IProductRepository _repository;

        public ProductWasIncludedConsumer(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Consume(ConsumeContext<IProductWasIncludedEvent> context)
        {
            var product = context.Message?.Data;
            await _repository.InsertAsync((Product)product);
        }
    }
}