using bShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace bShop.Data.BuilderConfigurations;

public static partial class BuilderConfigurations
{
    public static ModelBuilder CartConfiguration(this ModelBuilder builder) =>
        builder.Entity<Cart>(entity =>
        {
            entity.HasOne(x => x.User).WithMany(x => x.Carts).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        });
}
