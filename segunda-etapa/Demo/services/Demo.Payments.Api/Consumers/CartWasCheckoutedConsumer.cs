using Demo.Payments.Api.Infra;
using Demo.SharedModel.Contracts.Events.Carts;
using Demo.SharedModel.Contracts.Events.Payments;
using Demo.SharedModel.Events.Payments;
using Demo.SharedModel.Models;
using MassTransit;

namespace Demo.Payments.Api.Consumers
{
    public class CartWasCheckoutedConsumer : IConsumer<ICartWasCheckouted>
    {
        private readonly IPaymentProvider _paymentProviver;
        private readonly IPaymentRepository _paymentRepository;

        public CartWasCheckoutedConsumer(IPaymentProvider paymentProviver, IPaymentRepository paymentRepository)
        {
            _paymentProviver = paymentProviver ?? throw new ArgumentNullException(nameof(paymentProviver));
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
        }

        public async Task Consume(ConsumeContext<ICartWasCheckouted> context)
        {
            var cart = context.Message?.Data;            
            var payment = new Payment(cart.Customer, cart.Total, cart.Id);

            var isSuccess = await _paymentProviver.PayAsync(payment);            
            if (isSuccess)
            {
                // await _paymentRepository.SaveAsync(payment);
                await context.Publish<IPaymentWasApprovedEvent>(new PaymentWasApprovedEvent(payment));
            }
            else
                await context.Publish<IPaymentWasRejectedEvent>(new PaymentWasRejectedEvent(payment));
        }
    }
}