using bShop.Client.Models;
using System.Security.Claims;

namespace bShop.Client.Extensions;

public static class ClaimsPrincipalEctensions
{
    public static CurrnetUserModel GetCurrnetUserModel(this ClaimsPrincipal user)
    {
        _ = int.TryParse(user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? "", out int id);
        return new CurrnetUserModel
        {
            Id = id,
            UserName = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? "",
            Email = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "",
            FullName = user.Claims.FirstOrDefault(x => x.Type == nameof(CurrnetUserModel.FullName))?.Value ?? "",
            FirstName = user.Claims.FirstOrDefault(x => x.Type == nameof(CurrnetUserModel.FirstName))?.Value ?? "",
            PhoneNumber = user.Claims.FirstOrDefault(x => x.Type == nameof(CurrnetUserModel.PhoneNumber))?.Value ?? "",
            User = user,
        };
    }
}
