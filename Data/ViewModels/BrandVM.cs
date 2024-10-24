using bShop.Data.Interfaces;

namespace bShop.Data.ViewModels;

public class BrandVM : ISelectable<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;

    public bool Active { get; set; }
    public bool Selected { get; set; }

    public string? Image { get; set; }
    public int? Version { get; set; }

    public int ProductsCount { get; set; }
    public List<ProductVM> Products { get; set; } = [];
}
