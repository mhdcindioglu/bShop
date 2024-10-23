using bShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace bShop.Data.BuilderConfigurations;

public static partial class BuilderConfigurations
{
    public static ModelBuilder CategoryConfiguration(this ModelBuilder builder) =>
        builder.Entity<Category>(entity =>
        {
        });
}
