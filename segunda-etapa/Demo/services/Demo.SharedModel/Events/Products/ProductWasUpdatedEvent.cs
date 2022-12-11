using Demo.SharedModel.Contracts.Events.Products;
using Demo.SharedModel.Models;

namespace Demo.SharedModel.Events.Products
{
    public class ProductWasUpdatedEvent : EventBase<Product>, IProductWasUpdatedEvent
    {
        public ProductWasUpdatedEvent(Product product) : base(product) 
            => Name = EventsConstants.EVENT_PRODUCT_WAS_UPDATED;
    }
}