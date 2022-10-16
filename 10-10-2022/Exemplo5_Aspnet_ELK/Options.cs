using System.Text.Json;
using Exemplo5_Aspnet_ELK;

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
    public JaegerOpt Jaeger { get; set; }
    public ZipkinOpt Zipkin { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public bool IsServer() => Mode == AppMode.SERVER;
    public bool IsClient() => Mode == AppMode.CLIENT;
}

internal class JaegerOpt
{
    public string Url { get; set; }
    public int Port { get; set; }
}

internal class ZipkinOpt
{
    public string Url { get; set; }
    public int Port { get; set; }

    public Uri Uri => new Uri($"{Url}:{Port}/api/v2/spans");

}