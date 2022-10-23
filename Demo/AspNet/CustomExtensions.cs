using Microsoft.OpenApi.Models;

internal static class CustomExtensions
{
    internal static IServiceCollection AddOptions(this IServiceCollection self, IConfiguration configuration)
    {
        self.Configure<Options>(configuration.GetSection("Options"));
        return self;
    }

    internal static Options GetOptions(this IServiceCollection self, IConfiguration configuration)
    {
        var options = new Options();
        configuration.GetSection("Options").Bind(options);
        return options;
    }

    internal static IServiceCollection AddClients(this IServiceCollection self)
    {
        self.AddHttpClient();
        self.AddSingleton<IPostClient, PostClient>();
        return self;
    }


    internal static IServiceCollection AddRepositories(this IServiceCollection self)
    {
        self.AddSingleton<IPostRepository, PostRepository>();
        self.AddAsyncInitializer<Seed>();
        return self;
    }

    internal static IServiceCollection AddServices(this IServiceCollection self)
    {
        self.AddSingleton<IPostService, PostService>();
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