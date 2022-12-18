using Demo.ProductStock.Api.Contracts.Commands;
using Demo.SharedModel.Contracts.Events.Payments;
using MassTransit;

namespace Demo.ProductStock.Api.Consumers
{
    public class PaymentWasApprovedConsumer : IConsumer<IPaymentWasApprovedEvent>
    {
        public async Task Consume(ConsumeContext<IPaymentWasApprovedEvent> context)
        {
            var payment = context.Message?.Data;
            foreach (var product in payment?.Items)
            {
                await context.Publish<IUpdateProductCommand>(new UpdateProductCommand(product));
            }
        }
    }
}