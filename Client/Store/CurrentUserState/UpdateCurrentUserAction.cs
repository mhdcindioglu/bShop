using bShop.Client.Models;

namespace bShop.Client.Store.CurrentUserState;

public class UpdateCurrentUserAction(CurrnetUserModel currnetUser)
{
    public CurrnetUserModel CurrnetUser { get; set; } = currnetUser;
}
