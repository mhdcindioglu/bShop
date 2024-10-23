namespace bShop.Data.Entities;

public class UserWhiteList
{
    public int UserId { get; set; }
    public ShopUser? User { get; set; }
    
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}
