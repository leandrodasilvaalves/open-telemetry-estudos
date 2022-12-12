namespace Demo.ProductCatalog.Api.Config
{
    public class RedisConfig
    {
        public string InstanceName { get; set; }
        public string ConnectionString { get; set; }
        public int TTL { get; set; }
    }
}