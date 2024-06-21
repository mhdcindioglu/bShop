using bShop.Server.Data;
using bShop.Server.Data.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace bShop.Server.Extensions;

public static class WebApplicationExtensions
{
    public static async Task<IApplicationBuilder> UseAppDbMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<ShopDB>();
        DbInitializer.MigrationDB(db);

        using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ShopUser>>();
        using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ShopRole>>();
        await DbInitializer.EnsureDataAdded(db, userManager, roleManager);
        return app;
    }

    public static IApplicationBuilder UseAppSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}
