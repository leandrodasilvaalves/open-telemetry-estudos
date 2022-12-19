using Demo.SharedModel.Contracts.Events.Carts;
using Demo.SharedModel.Models;

namespace Demo.SharedModel.Events.Carts
{
    public class CartWasCheckouted : EventBase<Cart>, ICartWasCheckouted
    {
        public CartWasCheckouted(Cart cart) : base(cart) 
            => Name = EventsConstants.EVENT_CART_WAS_CHECKOUTED;
    }
}
