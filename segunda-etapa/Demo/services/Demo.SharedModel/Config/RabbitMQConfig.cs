namespace Demo.SharedModel.Config
{
    public class RabbitMQConfig
    {
        public string Cluster { get; set; }
        public string[] Hosts { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}