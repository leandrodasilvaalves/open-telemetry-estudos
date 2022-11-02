using System.Text.Json;

public class Options
{
    public Options() { }

    private string _serviceName;
    public string ServiceName
    {
        get => $"Lele.Servico.{_serviceName}";
        set => _serviceName = value;
    }

    public string ServiceVersion { get; set; }
    public string UrlClient { get; set; }
    public int MaxDelayMileseconds { get; set; }
    public string OtelUrl { get; set; }
    public AppMode Mode { get; set; }
    public SeedOptions Seed { get; set; }
    public bool UseCache { get; set; } = false;
    public RedisOptions Redis { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public bool IsServer() => Mode == AppMode.SERVER;
    public bool IsClient() => Mode == AppMode.CLIENT;
}

public class SeedOptions
{
    public int Records { get; set; }
    public int MaxBodyWords { get; set; }
}

public class RedisOptions
{    
    public string InstanceName { get; set; }
    public string ConnectionString { get; set; }
    public int TTL { get; set; }
}