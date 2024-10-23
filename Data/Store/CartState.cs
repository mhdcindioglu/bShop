using bShop.Data.Entities;
using Fluxor;

namespace bShop.Data.Store;

public record UpdateCartAction(Cart? Cart);


[FeatureState]
public record CartState
{
    public Cart? Cart { get; }

    private CartState() { }

    public CartState(Cart? cart)
    {
        Cart = cart;
    }
}
public static class CartStateReducers
{
    [ReducerMethod]
    public static CartState ReduceUpdateCartAction(CartState state, UpdateCartAction action) =>
        new(cart: action.Cart);
}

