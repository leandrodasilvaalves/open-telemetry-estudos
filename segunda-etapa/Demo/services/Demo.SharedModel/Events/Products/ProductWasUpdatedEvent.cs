using Demo.SharedModel.Models;

namespace Demo.SharedModel.Events.Products
{
    public class ProductWasUpdatedEvent : EventBase
    {
        public ProductWasUpdatedEvent(Product product) : base(product) 
            => Name = EventsConstants.EVENT_PRODUCT_WAS_UPDATED;
    }
}