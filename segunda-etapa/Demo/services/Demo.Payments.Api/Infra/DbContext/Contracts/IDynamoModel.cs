using Amazon.DynamoDBv2.DataModel;

namespace Demo.Payments.Api.Infra.DbContext.Contracts
{
    public interface IDynamoModel<TPartitionKey, TSortKey> : IDynamoModel
    {
        [DynamoDBHashKey]
        public TPartitionKey PartitionKey { get; set; }

        [DynamoDBRangeKey]
        public TSortKey SortKey { get; set; }
    }

    public interface IDynamoModel { }
}