using Microsoft.Extensions.Logging;

namespace Demo.OpenTelemetry.Config
{
    public class OpenTelemetryOptions
    {
        public string ServiceName { get; set; }
        public string Version { get; set; }
        public string CollectorUrl { get; set; }
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
    }
}