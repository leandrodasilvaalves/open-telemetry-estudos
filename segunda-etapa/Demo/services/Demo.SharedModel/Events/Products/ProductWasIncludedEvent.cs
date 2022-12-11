using Demo.SharedModel.Models;

namespace Demo.SharedModel.Events.Products
{
    public class ProductWasIncludedEvent : EventBase
    {
        public ProductWasIncludedEvent(Product product) : base(product) 
            => Name = EventsConstants.EVENT_PRODUCT_WAS_INCLUDED;
    }
}