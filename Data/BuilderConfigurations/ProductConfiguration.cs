using bShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace bShop.Data.BuilderConfigurations;

public static partial class BuilderConfigurations
{
    public static ModelBuilder ProductConfiguration(this ModelBuilder builder) =>
        builder.Entity<Product>(entity =>
        {
            entity.HasOne(x => x.Brand).WithMany(x => x.Products).HasForeignKey(x => x.BrandID).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(x => x.Collection).WithMany(x => x.Products).HasForeignKey(x => x.CollectionId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(x => x.Label).WithMany(x => x.Products).HasForeignKey(x => x.LabelId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(x => x.Vendor).WithMany(x => x.Products).HasForeignKey(x => x.VendorID).OnDelete(DeleteBehavior.NoAction);
        });
}
