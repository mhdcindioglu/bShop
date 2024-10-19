using bShop.Data.Enums;
using bShop.Data.ViewModels;

namespace bShop.Data.Filters;

public class ProductsFilter : BaseFilter
{
    public int Id { get; set; }
    public bool WithProducts { get; set; }

    public List<CategoryVM> Categories { get; set; } = [];
    public List<BrandVM> Brands { get; set; } = [];
}
