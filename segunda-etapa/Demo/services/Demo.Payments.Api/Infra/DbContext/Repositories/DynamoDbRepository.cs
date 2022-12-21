using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Demo.Payments.Api.Infra.DbContext.Contracts;

namespace Demo.Payments.Api.Infra.DbContext.Repositories
{
    public class DynamoDbRepository : IDynamoDbRepository
    {
        private readonly IDynamoDBContext _context;

        public DynamoDbRepository(IAmazonDynamoDB client, DynamoDBContextConfig contextConfiguration)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (contextConfiguration is null) { throw new ArgumentNullException(nameof(contextConfiguration)); }

            _context = new DynamoDBContext(client, contextConfiguration);
        }

        public Task SaveAsync<TModel>(TModel model, DynamoDBOperationConfig configuration = null) 
            where TModel : class, IDynamoModel
            => _context.SaveAsync(model, configuration);      
        

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) { return; }

            if (disposing) { _context?.Dispose(); }

            _disposed = true;
        }

        ~DynamoDbRepository() => Dispose(false);
    }

}