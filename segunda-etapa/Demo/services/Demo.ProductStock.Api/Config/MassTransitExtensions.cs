using Demo.ProductStock.Api.Consumers;
using Demo.ProductStock.Api.Contracts.Commands;
using Demo.SharedModel.Config;
using Demo.SharedModel.Contracts.Events;
using Demo.SharedModel.Events;
using Demo.SharedModel.Models;
using MassTransit;

namespace Demo.ProductStock.Api.Config
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<PaymentWasApprovedConsumer>();
                configure.AddConsumer<UpdateProductCommandConsumer>();

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

                    configureBus.ReceiveEndpoint(EventsConstants.ENDPOINT_PRODUCT_STOCK_RECEIVED_NOTIFICATIONS, endpoint =>
                    {
                        endpoint.ConfigureConsumer<PaymentWasApprovedConsumer>(provider);
                    });

                    configureBus.ReceiveEndpoint(EventsConstants.ENDPOINT_PRODUCT_STOCK_RECEIVED_COMMANDS, endpoint =>
                    {
                        endpoint.Consumer<UpdateProductCommandConsumer>(provider);
                    });

                    configureBus.Message<IEventBase<Product>>(x => { x.SetEntityName(EventsConstants.ENDPOINT_PRODUCT_STOCK_EVENTS); });                    
                    configureBus.Message<IEventBase<LogisticNotification>>(x => { x.SetEntityName(EventsConstants.ENDPOINT_PRODUCT_STOCK_EVENTS); });                    
                }));
            });
            return services;
        }
    }
}