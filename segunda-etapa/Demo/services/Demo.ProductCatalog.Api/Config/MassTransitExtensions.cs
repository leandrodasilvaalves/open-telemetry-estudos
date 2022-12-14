using Demo.ProductCatalog.Api.Consumers;
using Demo.SharedModel.Config;
using Demo.SharedModel.Contracts.Events;
using Demo.SharedModel.Events;
using Demo.SharedModel.Models;
using MassTransit;

namespace Demo.ProductCatalog.Api.Config
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<ProductWasIncludedConsumer>();
                configure.AddConsumer<ProductWasUpdatedConsumer>();
                configure.AddConsumer<ProductWasExcludedConsumer>();

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

                    configureBus.ReceiveEndpoint(EventsConstants.ENDPOINT_PRODUCT_CATALOG_RECEIVE_NOTIFICATIONS, endpoint =>
                    {
                        endpoint.ConfigureConsumer<ProductWasIncludedConsumer>(provider);
                        endpoint.ConfigureConsumer<ProductWasUpdatedConsumer>(provider);
                        endpoint.ConfigureConsumer<ProductWasExcludedConsumer>(provider);
                    });

                    configureBus.Message<IEventBase<Cart>>(x => { x.SetEntityName(EventsConstants.ENDPOINT_PRODUCT_CATALOG_EVENTS); });
                }));
            });
            return services;
        }
    }
}