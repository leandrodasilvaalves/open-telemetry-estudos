using Demo.SharedModel.Models;

namespace Demo.SharedModel.Events.Products
{
    public class ProductWasExcludedEvent : EventBase
    {
        public ProductWasExcludedEvent(Product product) : base(product)
            => Name = EventsConstants.EVENT_PRODUCT_WAS_EXCLUDED;
    }
}