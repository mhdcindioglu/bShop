using bShop.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace bShop.Shared.Models.Users;
public class ResetRequestVM
{
    [ShopRequired]
    [EmailAddress, DataType(DataType.EmailAddress)]
    [ShopDisplayName(nameof(Resources.Email))]
    public string Email { get; set; } = string.Empty;
    public bool HideLinks { get; set; }
}
