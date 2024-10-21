using bShop.Data.Interfaces;

namespace bShop.Data.Entities;

public class Image : IUniqueEntity<int>
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public virtual Product? Product { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
}
