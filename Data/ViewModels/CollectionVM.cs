using bShop.Data.Entities;

namespace bShop.Data.ViewModels;

public class CollectionVM
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ProductVM> Products { get; set; } = [];
}