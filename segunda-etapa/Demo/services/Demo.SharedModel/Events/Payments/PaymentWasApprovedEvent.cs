using Demo.SharedModel.Contracts.Events.Payments;
using Demo.SharedModel.Models;

namespace Demo.SharedModel.Events.Payments
{
    public class PaymentWasApprovedEvent : EventBase<Payment>, IPaymentWasApprovedEvent
    {
        public PaymentWasApprovedEvent(Payment data) : base(data)
            => Name = EventsConstants.EVENT_PAYMENT_WAS_APPROVED;
       
    }
}