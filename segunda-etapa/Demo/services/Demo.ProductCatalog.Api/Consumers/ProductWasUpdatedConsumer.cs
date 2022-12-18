using Demo.ProductCatalog.Api.Infra.Repository;
using Demo.ProductCatalog.Api.Models;
using Demo.SharedModel.Contracts.Events.Products;
using MassTransit;

namespace Demo.ProductCatalog.Api.Consumers
{
    public class ProductWasUpdatedConsumer : IConsumer<IProductWasUpdatedEvent>
    {
        private readonly IProductRepository _repository;

        public ProductWasUpdatedConsumer(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Consume(ConsumeContext<IProductWasUpdatedEvent> context)
        {
            var product = context.Message?.Data;
            await _repository.UpdateAsync((Product)product);
        }
    }
}