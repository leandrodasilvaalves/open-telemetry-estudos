using OpenTelemetry;
using OpenTelemetry.Logs;

namespace UsingLogs
{
    public class LogProcessor : BaseProcessor<LogRecord>
    {
        public override void OnStart(LogRecord data)
        {
            System.Console.WriteLine("Hello - OnStart");
            base.OnStart(data);
        }
        
        public override void OnEnd(LogRecord data)
        {
            System.Console.WriteLine("Hello - OnEnd");
            base.OnEnd(data);
        }
    }
}