using bShop.Client.Models;

namespace bShop.Client.Store.CurrentUserState;

[FeatureState]
public class CurrentUserState
{
    public CurrnetUserModel CurrnetUser { get; } = new();

    public CurrentUserState() { }
    public CurrentUserState(CurrnetUserModel currnetUser) { CurrnetUser = currnetUser; }
}
