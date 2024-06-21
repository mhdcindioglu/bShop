using bShop.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace bShop.Shared.Models.Users;
public class ChangePasswordRequestVM
{
    public int ID { get; set; }

    [ShopRequired, DataType(DataType.Password)]
    [ShopDisplayName(nameof(Resources.OldPassword))]
    public string OldPassword { get; set; } = string.Empty;

    [ShopRequired, DataType(DataType.Password)]
    [ShopDisplayName(nameof(Resources.NewPassword))]
    public string NewPassword { get; set; } = string.Empty;

    [Compare(nameof(NewPassword)), DataType(DataType.Password)]
    [ShopDisplayName(nameof(Resources.ConfirmPassword))]
    public string ConfirmPassword { get; set; } = string.Empty;
}
