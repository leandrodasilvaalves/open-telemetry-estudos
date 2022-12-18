using Demo.ProductStock.Api.Config;
using Demo.ProductStock.Api.Models;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace Demo.ProductStock.Api.Infra.Providers
{
    public interface IExternalLogisticProvider
    {
        Task<bool> NotifyAsync(LogisticNotification notification);
    }

    public class ExternalLogisticProvider : IExternalLogisticProvider
    {
        private readonly LogisticProviderOptions _options;

        public ExternalLogisticProvider(IOptions<LogisticProviderOptions> options)
        {
            if (options.Value is null) throw new ArgumentNullException(nameof(LogisticProviderOptions));
            _options = options.Value;
        }

        public async Task<bool> NotifyAsync(LogisticNotification notification)
        {
            var result = await $"{_options.Url}/order"
                .AllowAnyHttpStatus()
                .PostJsonAsync(notification)
                .ReceiveJson<LogisticProviderModel>();
            
            return result.Received;
        }
    }
}