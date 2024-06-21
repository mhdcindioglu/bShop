using bShop.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace bShop.Shared.Models.Users;
public class ResetPasswordRequestVM
{
    public int ID { get; set; }
    public string Code { get; set; } = string.Empty;

    [Required, DataType(DataType.Password)]
    [ShopDisplayName(nameof(Resources.NewPassword))]
    public string NewPassword { get; set; } = string.Empty;

    [Compare(nameof(NewPassword)), DataType(DataType.Password)]
    [ShopDisplayName(nameof(Resources.ConfirmPassword))]
    public string ConfirmPassword { get; set; } = string.Empty;
}
