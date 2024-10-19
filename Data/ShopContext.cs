using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using bShop.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace bShop.Data
{
    public class ShopContext(DbContextOptions<ShopContext> options) : IdentityDbContext<ShopUser, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>(options)
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Label> Labels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            builder.Entity<Product>(entity =>
            {
                entity.HasOne(x => x.Brand).WithMany(x => x.Products).HasForeignKey(x => x.BrandID).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.Collection).WithMany(x => x.Products).HasForeignKey(x => x.CollectionId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.Label).WithMany(x => x.Products).HasForeignKey(x => x.LabelId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.Vendor).WithMany(x => x.Products).HasForeignKey(x => x.VendorID).OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
