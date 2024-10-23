using bShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bShop.Data.BuilderConfigurations;

public static partial class BuilderConfigurations
{
    public static ModelBuilder IdentityUserLoginConfiguration(this ModelBuilder builder) =>
        builder.Entity<IdentityUserLogin<int>>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
        });
}
