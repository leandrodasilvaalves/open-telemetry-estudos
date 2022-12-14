using Demo.SharedModel.Contracts.Events.Carts;
using Demo.SharedModel.Contracts.Events.Products;
using Demo.SharedModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.SharedModel.Events.Carts
{
    public class CartWasCheckouted : EventBase<Cart>, ICartWasCheckouted
    {
        public CartWasCheckouted(Cart cart) : base(cart) 
            => Name = EventsConstants.EVENT_CART_WAS_CHECKOUTED;
    }
}
