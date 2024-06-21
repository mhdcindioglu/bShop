using Microsoft.AspNetCore.Identity;

namespace bShop.Server.Data.Entities.Users;

public class UserClaim : IdentityUserClaim<int>
{
    public virtual List<ShopUser> Users { get; set; } = [];
}
