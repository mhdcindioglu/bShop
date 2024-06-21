using bShop.Server.Data.Entities.Languages;
using bShop.Server.Data.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bShop.Server.Data;

public static class DbInitializer
{
    public static void MigrationDB(ShopDB context)
    {
        if (context.Database.GetPendingMigrations().Any())
            context.Database.Migrate();
    }

    public static async Task EnsureDataAdded(ShopDB context, UserManager<ShopUser> userManager, RoleManager<ShopRole> roleManager)
    {
        await context.Database.BeginTransactionAsync();

        try
        {
            await EnsureRoleCreated(roleManager, "Admin");
            await EnsureRoleCreated(roleManager, "Vendor");

            await EnsureUserCreated(userManager, "Admin");

            await EnsureLanguagesCreated(context);

            await context.Database.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException);
            await context.Database.RollbackTransactionAsync();
        }
    }

    private static async Task<ShopRole?> EnsureRoleCreated(RoleManager<ShopRole> roleManager, string roleName)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role != null)
            return role;

        role = new ShopRole(roleName);
        var result = await roleManager.CreateAsync(role);
        if (!result.Succeeded)
            return null;

        return role;
    }

    private static async Task<ShopUser?> EnsureUserCreated(UserManager<ShopUser> userManager, string userName)
    {
        var user = await userManager.FindByNameAsync($"{userName.ToLower()}@email.com");
        if (user != null)
            return user;

        user = new ShopUser
        {
            Email = $"{userName.ToLower()}@email.com",
            UserName = $"{userName.ToLower()}@email.com",
            EmailConfirmed = true,
            FullName = userName,
            PasswordUpdated = true,
        };
        var result = await userManager.CreateAsync(user, "P@ssw0rd");
        if (!result.Succeeded)
            return null;

        user = await userManager.FindByEmailAsync(user.Email);
        if (user == null)
            return null;

        result = await userManager.AddToRoleAsync(user, userName);
        if (!result.Succeeded)
            return null;

        return user;
    }

    private static async Task EnsureLanguagesCreated(ShopDB context)
    {
        if (!context.Languages.Any(x => x.Name == "English"))
            await context.AddAsync(new Language { Name = "English", Culture = "en-US", SeoCode = "en", Rtl = false, IsActive = true, IsDeleted = false, DisplayOrder = 0, });

        if (!context.Languages.Any(x => x.Name == "العربية"))
            await context.AddAsync(new Language { Name = "العربية", Culture = "ar-AE", SeoCode = "ar", Rtl = true, IsActive = true, IsDeleted = false, DisplayOrder = 1, });

        if (!context.Languages.Any(x => x.Name == "English") || !context.Languages.Any(x => x.Name == "العربية"))
            await context.SaveChangesAsync();
    }
}
