using Amazon.DynamoDBv2.DataModel;
using Demo.Payments.Api.Infra.DbContext.Contracts;
using Demo.Payments.Api.Infra.DbContext.DynamoDb.Tables;
using Demo.SharedModel.Models;

namespace Demo.Payments.Api.Infra.DbContext.Models
{
    [DynamoDBTable(TableNames.PAYMENTS_TABLE)]
    public class PaymentDbModel : IDynamoModel<string, string>
    {
        public string PartitionKey { get; set; }
        public string SortKey { get; set; }
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Customer Customer { get; set; }
        public float Amount { get; set; }
        public List<CartItem> Items { get; set; }
    }
}