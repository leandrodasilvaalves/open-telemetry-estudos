using Demo.SharedModel.Models;

namespace Demo.SharedModel.Contracts.Events.Products
{
    public interface IProductWasIncludedEvent : IEventBase<Product>
    {
    }
}