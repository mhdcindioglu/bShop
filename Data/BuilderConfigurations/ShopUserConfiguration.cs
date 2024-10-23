using bShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace bShop.Data.BuilderConfigurations;

public static partial class BuilderConfigurations
{
    public static ModelBuilder ShopUserConfiguration(this ModelBuilder builder) =>
        builder.Entity<ShopUser>(entity =>
        {
        });
}
