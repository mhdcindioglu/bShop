using bShop.Data.Interfaces;

namespace bShop.Data.Entities;

public class Category : IUniqueEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;

    public virtual List<Product> Products { get; set; } = [];
}
