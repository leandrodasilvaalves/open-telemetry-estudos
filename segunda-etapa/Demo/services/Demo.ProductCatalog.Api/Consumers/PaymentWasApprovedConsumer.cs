using Demo.ProductCatalog.Api.Infra.Repository;
using Demo.SharedModel.Contracts.Events.Payments;
using Demo.SharedModel.Models;
using MassTransit;

namespace Demo.ProductCatalog.Api.Consumers
{
    public class PaymentWasApprovedConsumer : IConsumer<IPaymentWasApprovedEvent>
    {
        private readonly ICartRepository _cartRepository;

        public PaymentWasApprovedConsumer(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        public async Task Consume(ConsumeContext<IPaymentWasApprovedEvent> context)
        {
            var payment = context.Message?.Data;
            await _cartRepository.InsertAsync(new Models.Cart
            {
                Customer = payment.Customer,
                Items = payment.Items,
                PaymentId = payment.Id,
                CartId = payment.CartId,
                Status = CartStatus.CLOSED,
            });            
        }
    }
}