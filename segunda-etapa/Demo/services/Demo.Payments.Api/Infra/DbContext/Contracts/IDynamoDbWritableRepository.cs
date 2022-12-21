using Amazon.DynamoDBv2.DataModel;

namespace Demo.Payments.Api.Infra.DbContext.Contracts
{
    public interface IDynamoDbWritableRepository
    {
        Task SaveAsync<TModel>(TModel model, DynamoDBOperationConfig configuration = null) where TModel : class, IDynamoModel;
    }
}