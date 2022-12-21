namespace Demo.Payments.Api.Infra.DbContext.Contracts
{
    public interface IDynamoDbRepository : IDynamoDbWritableRepository, IDisposable { }
}