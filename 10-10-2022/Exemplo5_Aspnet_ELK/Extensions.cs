using Microsoft.OpenApi.Models;

namespace Exemplo5_Aspnet_ELK
{
    internal static class Extensions
    {
        internal static Options GetOptions(this IServiceCollection self, IConfiguration configuration)
        {
            self.Configure<Options>(
            configuration.GetSection("Options"));

            var options = Options.GetInstance();
            configuration.GetSection("Options").Bind(options);
            return options;
        }

        internal static IServiceCollection AddClients(this IServiceCollection self)
        {
            self.AddHttpClient();
            return self;
        }


        internal static IServiceCollection AddRepositories(this IServiceCollection self)
        {
            self.AddSingleton<IPostRepository, PostRepository>();
            return self;
        }

        internal static IServiceCollection AddSwagger(this IServiceCollection self, Options options)
        {
            self.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = options.ServiceName, Description = "Demo Opentelemetry", Version = "v1" });
            });
            return self;
        }

        internal static void ConfigureSwagger(this WebApplication app, Options options)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{options.ServiceName} - Opentelemetry - v1");
            });
        }
    }
}