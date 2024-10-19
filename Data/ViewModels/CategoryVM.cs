using bShop.Data.Interfaces;

namespace bShop.Data.ViewModels;

public class CategoryVM : ISelectable<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int ProductsCount { get; set; } 
    public bool Selected { get; set; }

    public List<ProductCardVM> Products { get; set; } = [];
}
