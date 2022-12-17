using Demo.Payments.Api.Consumers;
using Demo.SharedModel.Config;
using Demo.SharedModel.Contracts.Events;
using Demo.SharedModel.Events;
using Demo.SharedModel.Models;
using MassTransit;

namespace Demo.Payments.Api.Config
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<CartWasCheckoutedConsumer>();

                configure.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(configureBus =>
                {
                    var config = configuration.GetRabbitMQConfig();
                    var uri = new Uri(config.Cluster);
                    configureBus.Host(uri, rabbitMq =>
                    {
                        rabbitMq.Username(config.Username);
                        rabbitMq.Password(config.Password);
                        rabbitMq.UseCluster(clusterConfig =>
                            config.Hosts.ToList().ForEach(host => clusterConfig.Node(host)));
                    });

                    configureBus.ReceiveEndpoint(EventsConstants.ENDPOINT_PAYMENT_RECEIVE_NOTIFICATIONS, endpoint =>
                    {
                        endpoint.ConfigureConsumer<CartWasCheckoutedConsumer>(provider);
                    });

                    configureBus.Message<IEventBase<Payment>>(x => { x.SetEntityName(EventsConstants.ENDPOINT_PAYMENTS_EVENTS); });
                }));
            });
            return services;
        }
    }
}
