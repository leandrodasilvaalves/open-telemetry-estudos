namespace Demo.ProductCatalog.Api.Config
{
    public static class RedisExtensions
    {
        public static IServiceCollection AddRedids(this IServiceCollection services, IConfiguration config)
        {
            var redisOptions = services
                .AddOptions<RedisConfig>(config)
                .GetOptions<RedisConfig>(config);

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = redisOptions.ConnectionString;
                options.InstanceName = redisOptions.InstanceName;
            });
            return services;
        }
    }
}