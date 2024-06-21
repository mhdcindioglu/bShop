using bShop.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace bShop.Shared.Models.Users;
public class LoginRequestVM
{
    [ShopRequired, EmailAddress, DataType(DataType.EmailAddress)]
    [ShopDisplayName(nameof(Resources.UserName))]
    public string UserName { get; set; } = "";

    [ShopRequired, DataType(DataType.Password)]
    [ShopDisplayName(nameof(Resources.Password))]
    public string Password { get; set; } = "";

    [ShopDisplayName(nameof(Resources.RememberMe))]
    public bool RememberMe { get; set; }
}
