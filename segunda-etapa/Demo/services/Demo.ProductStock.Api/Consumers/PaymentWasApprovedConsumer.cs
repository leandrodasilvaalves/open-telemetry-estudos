using Demo.ProductStock.Api.Contracts.Commands;
using Demo.ProductStock.Api.Infra.Providers;
using Demo.ProductStock.Api.Models;
using Demo.SharedModel.Contracts.Events.Payments;
using MassTransit;

namespace Demo.ProductStock.Api.Consumers
{
    public class PaymentWasApprovedConsumer : IConsumer<IPaymentWasApprovedEvent>
    {
        private readonly IExternalLogisticProvider _externalLogisticProvider;

        public PaymentWasApprovedConsumer(IExternalLogisticProvider externalLogisticProvider)
        {
            _externalLogisticProvider = externalLogisticProvider ?? throw new ArgumentNullException(nameof(externalLogisticProvider));
        }

        public async Task Consume(ConsumeContext<IPaymentWasApprovedEvent> context)
        {
            var payment = context.Message?.Data;
            await _externalLogisticProvider.NotifyAsync(new LogisticNotification
            {
                Customer = payment.Customer,
                Itens = payment.Items,
            });

            foreach (var product in payment?.Items)
            {
                await context.Publish<IUpdateProductCommand>(new UpdateProductCommand(product));
            }
        }
    }
}