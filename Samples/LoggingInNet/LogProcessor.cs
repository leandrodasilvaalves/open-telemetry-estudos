using OpenTelemetry;
using OpenTelemetry.Logs;

namespace LoggingInNet
{
    public class LogProcessor : BaseProcessor<LogRecord>
    {
        public override void OnEnd(LogRecord data)
        {
            Console.WriteLine("Hello - OnEnd");
            base.OnEnd(data);
        }
    }
}