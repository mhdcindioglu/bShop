using bShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace bShop.Data.BuilderConfigurations;

public static partial class BuilderConfigurations
{
    public static ModelBuilder UserWhiteListConfiguration(this ModelBuilder builder) =>
        builder.Entity<UserWhiteList>(entity =>
        {
            entity.HasKey(x => new { x.ProductId, x.UserId });
        });
}
