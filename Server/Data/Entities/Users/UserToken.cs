using Microsoft.AspNetCore.Identity;

namespace bShop.Server.Data.Entities.Users;
public class UserToken : IdentityUserToken<int>
{
    public virtual List<ShopUser> Users { get; set; } = [];
}
