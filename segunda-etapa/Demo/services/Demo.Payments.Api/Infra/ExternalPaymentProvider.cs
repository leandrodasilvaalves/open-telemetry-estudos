using Demo.Payments.Api.Config;
using Demo.Payments.Api.Models;
using Demo.SharedModel.Models;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace Demo.Payments.Api.Infra
{
    public interface IPaymentProvider
    {
        Task<bool> PayAsync(Payment payment);
    }

    public class ExternalPaymentProvider : IPaymentProvider
    {
        private readonly PaymentProviderOptions _options;

        public ExternalPaymentProvider(IOptions<PaymentProviderOptions> options)
        {
            if (options.Value is null) throw new ArgumentNullException(nameof(PaymentProviderOptions));
            _options = options.Value;
        }

        public async Task<bool> PayAsync(Payment payment)
        {
            var result = await $"{_options.Url}/payments"
                .AllowAnyHttpStatus()
                .PostJsonAsync(payment)
                .ReceiveJson<PaymentProviderModel>();
                            
            return result.Status.Equals(PaymentProviderStatus.APPROVED);
        }
    }
}