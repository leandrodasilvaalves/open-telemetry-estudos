namespace Demo.Payments.Api.Infra.DbContext.DynamoDb
{
    public interface IDatabaseContext
    {
        Task ConfigureAsync();

        Task<bool> TableExist(string tableName);
    }
}