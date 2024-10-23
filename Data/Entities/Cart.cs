using bShop.Data.Interfaces;

namespace bShop.Data.Entities;

public class Cart : IUniqueEntity<int>
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public ShopUser? User { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime? OrderDate { get; set; }

    public virtual List<CartItem> CartItems { get; set; } = [];
}
