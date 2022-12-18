using System.Text.Json.Serialization;
using Demo.SharedModel.Models;

namespace Demo.ProductStock.Api.Contracts.Commands
{   
    public interface IUpdateProductCommand
    {
        CartItem Product { get; set; }
    }

    public class UpdateProductCommand : IUpdateProductCommand
    {
        public CartItem Product { get; set; }

        [JsonConstructor]
        public UpdateProductCommand(){}
        
        public UpdateProductCommand(CartItem product)
        {
            Product = product;
        }
    }
}