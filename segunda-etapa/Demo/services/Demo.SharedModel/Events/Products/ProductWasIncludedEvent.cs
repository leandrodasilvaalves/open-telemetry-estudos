using Demo.SharedModel.Contracts.Events.Products;
using Demo.SharedModel.Models;

namespace Demo.SharedModel.Events.Products
{
    public class ProductWasIncludedEvent : EventBase<Product>, IProductWasIncludedEvent
    {
        public ProductWasIncludedEvent(Product product) : base(product) 
            => Name = EventsConstants.EVENT_PRODUCT_WAS_INCLUDED;
    }
}