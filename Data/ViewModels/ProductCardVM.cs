using bShop.Data.Entities;
using System.Linq;

namespace bShop.Data.ViewModels;

public class ProductCardVM
{
    public int Id { get; set; }
    public List<ProductCardLabelVM> Labels { get; set; } = [];

    public int CategoryId { get; set; }
    public CategoryVM? Category { get; set; }

    public bool IsInCart { get; set; }
    public bool IsInWishList { get; set; }
    public string Colors { get; set; } = string.Empty;
    public string Link => $"/Product/{Id}/{Slug}";
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;

    public int BrandID { get; set; }
    public string BrandName { get; set; } = string.Empty;

    public int VendorID { get; set; }
    public string VendorName { get; set; } = string.Empty;

    public decimal SalePrice { get; set; }
    public decimal? OfferPrice { get; set; }

    public int RatesCount => CartItems.Count(x => x.Rate.HasValue);
    public double RatesAverage => CartItems.Where(x => x.Rate.HasValue).Average(x => x.Rate!.Value);

    public List<Image> Images { get; set; } = [];
    public virtual List<CartItem> CartItems { get; set; } = [];
}
