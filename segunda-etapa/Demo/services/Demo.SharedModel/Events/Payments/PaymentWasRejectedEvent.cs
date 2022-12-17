using Demo.SharedModel.Contracts.Events.Payments;
using Demo.SharedModel.Models;

namespace Demo.SharedModel.Events.Payments
{
    public class PaymentWasRejectedEvent : EventBase<Payment>, IPaymentWasRejectedEvent
    {
        public PaymentWasRejectedEvent(Payment data) : base(data)
            => Name = EventsConstants.EVENT_PAYMENT_WAS_REJECTED;
    }
}