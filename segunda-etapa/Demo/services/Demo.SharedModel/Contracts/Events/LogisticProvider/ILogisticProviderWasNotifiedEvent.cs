using Demo.SharedModel.Models;

namespace Demo.SharedModel.Contracts.Events.LogisticProvider
{
    public interface ILogisticProviderWasNotifiedEvent : IEventBase<LogisticNotification>
    {
    }
}