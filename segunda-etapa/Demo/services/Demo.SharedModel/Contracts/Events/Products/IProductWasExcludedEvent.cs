using Demo.SharedModel.Models;

namespace Demo.SharedModel.Contracts.Events.Products
{
    public interface IProductWasExcludedEvent : IEventBase<Product>
    {
    }
}