using bShop.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace bShop.Shared.Models.Users;
public class ProfileRequestVM
{
    public int ID { get; set; } 
 
    [ShopRequired]
    [ShopDisplayName(nameof(Resources.FullName))]
    public string FullName { get; set; } = "";

    [ShopDisplayName(nameof(Resources.UserName))]
    public string UserName { get; set; } = "";

    [ShopRequired, Phone, DataType(DataType.PhoneNumber)]
    [ShopDisplayName(nameof(Resources.PhoneNumber))]
    public string PhoneNumber { get; set; } = "";
    
    [ShopDisplayName(nameof(Resources.Address))]
    public string? Address { get; set; }
}
