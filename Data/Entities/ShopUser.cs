using Microsoft.AspNetCore.Identity;

namespace bShop.Data.Entities
{
    public class ShopUser : IdentityUser<int>
    {
        public virtual List<Cart> Carts { get; set; } = [];
    }
}
