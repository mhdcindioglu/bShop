using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using bShop.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace bShop.Data
{
    public class ShopContext(DbContextOptions<ShopContext> options) : IdentityDbContext<ShopUser, IdentityRole<int>, int>(options)
    {
    }
}
