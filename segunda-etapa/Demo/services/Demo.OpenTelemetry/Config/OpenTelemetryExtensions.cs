using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Demo.OpenTelemetry.Config
{
    public static class OpenTelemetryExtensions
    {
        private static OpenTelemetryOptions _options;
        private static ResourceBuilder _resource;

        public static WebApplicationBuilder AddOpenTelemetry(this WebApplicationBuilder builder)
        {
            _options = builder.Services
                    .AddOptions<OpenTelemetryOptions>(builder.Configuration)
                    .GetOptions<OpenTelemetryOptions>(builder.Configuration);

            _resource = ResourceBuilder
                    .CreateDefault()
                    .AddService(_options.ServiceName, _options.Version);

            builder.Services.AddTracing();
            // builder.Logging.AddLogging();
            // builder.Services.AddMetrics();                

            return builder;
        }

        public static void AddLogging(this ILoggingBuilder loggingBuilder)
        {
            loggingBuilder
                 .AddFilter<OpenTelemetryLoggerProvider>("*", _options.LogLevel)
                 .AddOpenTelemetry(builder =>
                 {
                     builder
                         .SetResourceBuilder(_resource)
                         .AddConsoleExporter()
                         .AddOtlpExporter(opt =>
                         {
                             opt.Endpoint = new Uri(_options.CollectorUrl);
                             opt.ExportProcessorType = ExportProcessorType.Simple;
                         })
                         .AddProcessor(new LogProcessor());

                     builder.IncludeFormattedMessage = true;
                     builder.ParseStateValues = true;
                 });
        }

        public static IServiceCollection AddTracing(this IServiceCollection services)
        {
            services.AddOpenTelemetryTracing(builder =>
            {
                builder
                    .AddConsoleExporter()
                    .AddSource(_options.ServiceName)
                    .SetResourceBuilder(_resource)
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddOtlpExporter(opt =>
                    {
                        opt.Endpoint = new Uri(_options.CollectorUrl);
                        opt.ExportProcessorType = ExportProcessorType.Simple;
                    });

            });
            return services;
        }

        public static IServiceCollection AddMetrics(this IServiceCollection services)
        {
            services.AddOpenTelemetryMetrics(builder =>
            {
                builder
                    .AddConsoleExporter()
                    .SetResourceBuilder(_resource)
                    .AddRuntimeInstrumentation();
            });
            return services;
        }
    }
}