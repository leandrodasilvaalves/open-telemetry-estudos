using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Demo.Payments.Api.Infra.DbContext.Contracts;
using Demo.Payments.Api.Infra.DbContext.DynamoDb;
using Demo.Payments.Api.Infra.DbContext.Models;
using Demo.SharedModel.Models;

namespace Demo.Payments.Api.Infra.DbContext.Repositories
{
    public interface IPaymentRepository
    {
        public Task SaveAsync(Payment payment);
    }

    public class PaymentRepository : IPaymentRepository
    {

        private readonly IDynamoDbRepository _repository;
        private readonly IDynamoDbFactoryModel<Payment, PaymentDbModel> _factory;
        private readonly DynamoDBOperationConfig _configuration;

        public PaymentRepository(IDynamoDbRepository repository, IDynamoDbFactoryModel<Payment, PaymentDbModel> factory, DynamoDbRepositoryOptions options)
        {
            if(options == null) throw new ArgumentNullException(nameof(options));

            _factory = factory ?? throw new ArgumentNullException(nameof(factory));            
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            _configuration = new DynamoDBOperationConfig
            {
                Conversion = DynamoDBEntryConversion.V2
            };
        }

        public async Task SaveAsync(Payment payment)
        {
            var model = _factory.ToModel(payment);
            await _repository.SaveAsync(model, _configuration);
        }
    }
}