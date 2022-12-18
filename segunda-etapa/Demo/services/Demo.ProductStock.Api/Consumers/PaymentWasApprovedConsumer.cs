using Demo.ProductStock.Api.Contracts.Commands;
using Demo.ProductStock.Api.Infra.Providers;
using Demo.SharedModel.Contracts.Events.LogisticProvider;
using Demo.SharedModel.Contracts.Events.Payments;
using Demo.SharedModel.Events.LogisticProvider;
using Demo.SharedModel.Models;
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
            var notification = new LogisticNotification(payment.Customer, payment.Items);
            await _externalLogisticProvider.NotifyAsync(notification);
            await context.Publish<ILogisticProviderWasNotifiedEvent>(new LogisticProviderWasNotifiedEvent(notification));

            foreach (var product in payment?.Items)
            {
                await context.Publish<IUpdateProductCommand>(new UpdateProductCommand(product));
            }
        }
    }
}