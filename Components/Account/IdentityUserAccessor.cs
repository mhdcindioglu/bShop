using bShop.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace bShop.Components.Account
{
    internal sealed class IdentityUserAccessor(UserManager<ShopUser> userManager, IdentityRedirectManager redirectManager)
    {
        public async Task<ShopUser> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            }

            return user;
        }
    }
}
