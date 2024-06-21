using bShop.Shared.Attributes;

namespace bShop.Shared.Models.Orders;
public class OrderTranckRequestVM
{
    [ShopRequired]
    [ShopDisplayName(nameof(Resources.OrderNo))]
    public string OrderNo { get; set; } = string.Empty;
    
    [ShopRequired]
    [ShopDisplayName(nameof(Resources.Email))]
    public string Email { get; set; } = string.Empty;
}
