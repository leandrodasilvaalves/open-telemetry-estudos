using OpenTelemetry;
using OpenTelemetry.Logs;

namespace Demo.OpenTelemetry.Config
{
    public class LogProcessor : BaseProcessor<LogRecord>
    {
        public override void OnEnd(LogRecord data)
        {
            System.Console.WriteLine("Something on end log");
            base.OnEnd(data);
        }
    }
}