using Demo.SharedModel.Events;
using MassTransit;

namespace Demo.ProductStock.Api.Config
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(configure =>
            {
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

                    configureBus.Message<EventBase>(x => { x.SetEntityName(EventsConstants.ENDPOINT_PRODUCT_STOCK); });
                }));
            });
            return services;
        }

        public static RabbitMQConfig GetRabbitMQConfig(this IConfiguration configuration)
           => configuration.GetSection("RabbitMQCluster").Get<RabbitMQConfig>();
    }
}