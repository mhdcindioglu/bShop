using bShop.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace bShop.Shared.Models.Users;
public class RegisterRequestVM
{
    [Required]
    [ShopDisplayName(nameof(Resources.FullName))]
    public string FullName { get; set; } = "";

    [Required, EmailAddress]
    [ShopDisplayName(nameof(Resources.UserName))]
    public string UserName { get; set; } = "";


    [Required, DataType(DataType.Password)]
    [ShopDisplayName(nameof(Resources.Password))]
    public string Password { get; set; } = "";


    [Compare(nameof(Password)), DataType(DataType.Password)]
    [ShopDisplayName(nameof(Resources.ConfirmPassword))]
    public string ConfirmPassword { get; set; } = "";
}
