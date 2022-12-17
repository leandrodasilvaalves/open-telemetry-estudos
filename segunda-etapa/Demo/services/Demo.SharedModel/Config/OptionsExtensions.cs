using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.SharedModel.Config
{
    public static class OptionsExtensions
    {
        public static IServiceCollection AddOptions<T>(this IServiceCollection self, IConfiguration configuration) where T : class
        {
            self.Configure<T>(configuration.GetSection(typeof(T).Name));
            return self;
        }

        public static T GetOptions<T>(this IServiceCollection self, IConfiguration configuration) where T : class
        {
            var type = typeof(T);
            var assembly = type.Assembly;
            var options = assembly.CreateInstance(type.FullName) as T;
            configuration.GetSection(type.Name).Bind(options);
            return options;
        }
    }
}