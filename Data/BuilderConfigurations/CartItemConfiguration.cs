using bShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace bShop.Data.BuilderConfigurations;

public static partial class BuilderConfigurations
{
    public static ModelBuilder CartItemConfiguration(this ModelBuilder builder) =>
        builder.Entity<CartItem>(entity =>
        {
            entity.HasOne(x => x.Product).WithMany(x => x.CartItems).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(x => x.Cart).WithMany(x => x.CartItems).HasForeignKey(x => x.CartId).OnDelete(DeleteBehavior.NoAction);
        });
}
