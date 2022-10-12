using System.Text.Json;
internal class Options
{
    private string _serviceName;
    public string ServiceName
    {
        get => $"Lele.Servico.{_serviceName}";
        set => _serviceName = value;
    }

    public string ServiceVersion { get; set; }

    public string UrlClient { get; set; }
    public string OtelUrl { get; set; }
    public JaegerOpt Jaeger { get; set; }
    public ZipkinOpt Zipkin { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
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