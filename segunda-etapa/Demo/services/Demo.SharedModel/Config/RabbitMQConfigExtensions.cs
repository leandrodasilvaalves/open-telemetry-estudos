using Microsoft.Extensions.Configuration;

namespace Demo.SharedModel.Config
{
    public static class RabbitMQConfigExtensions
    {
        public static RabbitMQConfig GetRabbitMQConfig(this IConfiguration configuration)
           => configuration.GetSection("RabbitMQCluster").Get<RabbitMQConfig>();
    }
}