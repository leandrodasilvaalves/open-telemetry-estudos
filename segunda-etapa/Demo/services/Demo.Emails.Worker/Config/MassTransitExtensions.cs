using Demo.Emails.Worker.Consumer;
using Demo.SharedModel.Config;
using Demo.SharedModel.Events;
using MassTransit;

namespace Demo.Emails.Worker.Config
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<PaymentWasApprovedConsumer>();
                configure.AddConsumer<PaymentWasRejectedConsumer>();

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

                    configureBus.ReceiveEndpoint(EventsConstants.ENDPOINT_EMAIL_RECEIVED_NOTIFICATIONS, endpoint =>
                    {
                        endpoint.ConfigureConsumer<PaymentWasApprovedConsumer>(provider);
                        endpoint.ConfigureConsumer<PaymentWasRejectedConsumer>(provider);
                    });                    
                }));
            });
            return services;
        }
    }
}