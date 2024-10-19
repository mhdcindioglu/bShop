using bShop.Data.Interfaces;

namespace bShop.Data.Entities;

public class Brand : IUniqueEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public bool Active { get; set; }

    public string? Image { get; set; } 
    public int? Version { get; set; }

    public virtual List<Product> Products { get; set; } = [];
}
