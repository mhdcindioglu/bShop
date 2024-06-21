using Microsoft.AspNetCore.Identity;

namespace bShop.Server.Data.Entities.Users;

public class UserLogin : IdentityUserLogin<int>
{
    public virtual List<ShopUser> Users { get; set; } = [];
}
