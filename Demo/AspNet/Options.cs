using System.Text.Json;
using AspNet;

internal class Options
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

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public bool IsServer() => Mode == AppMode.SERVER;
    public bool IsClient() => Mode == AppMode.CLIENT;
}

internal class SeedOptions
{
    public int Records { get; set; }
    public int MaxBodyWords { get; set; }
}