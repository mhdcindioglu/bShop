using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using bShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using bShop.Data.BuilderConfigurations;

namespace bShop.Data
{
    public class ShopContext(DbContextOptions<ShopContext> options) : IdentityDbContext<ShopUser, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>(options)
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<NewsLetter> NewsLetters { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserWhiteList> UserWhiteLists { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.BrandConfiguration();
            builder.CartConfiguration();
            builder.CartItemConfiguration();
            builder.CategoryConfiguration();
            builder.CollectionConfiguration();
            builder.ImageConfiguration();
            builder.LabelConfiguration();
            builder.NewsLetterConfiguration();
            builder.ProductConfiguration();
            builder.ShopUserConfiguration();
            builder.IdentityUserLoginConfiguration();
            builder.UserWhiteListConfiguration();
            builder.VendorConfiguration();
        }
    }
}
