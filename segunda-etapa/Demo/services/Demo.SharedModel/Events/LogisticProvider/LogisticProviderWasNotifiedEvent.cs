using Demo.SharedModel.Contracts.Events.LogisticProvider;
using Demo.SharedModel.Models;

namespace Demo.SharedModel.Events.LogisticProvider
{
    public class LogisticProviderWasNotifiedEvent : EventBase<LogisticNotification>, ILogisticProviderWasNotifiedEvent
    {
        public LogisticProviderWasNotifiedEvent(LogisticNotification data) : base(data)
            => Name = EventsConstants.EVENT_LOGISTIC_PROVIDER_WAS_NOTIFIED;
    }
}