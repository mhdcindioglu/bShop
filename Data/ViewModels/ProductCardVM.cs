using bShop.Data.Entities;

namespace bShop.Data.ViewModels;

public class ProductCardVM
{
    public ProductCardVM()
    {
        RatingCount = Random.Shared.Next(999);
        RatingRate = Random.Shared.Next(100);
    }
    public int Id { get; set; }
    public List<ProductCardLabelVM> Labels { get; set; } = [];
    
    public int CategoryId { get; set; }
    public CategoryVM? Category { get; set; }
    
    public bool IsInCart { get; set; }
    public bool IsInWishList { get; set; }
    public bool ShowRating { get; set; } = true;
    public string Colors { get; set; } = string.Empty;
    public string Link => $"/Product/{Id}/{Slug}";
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;

    public int BrandID { get; set; }
    public string BrandName { get; set; } = string.Empty;

    public int VendorID { get; set; }
    public string VendorName { get; set; } = string.Empty;

    public decimal SalePrice { get; set; }
    public decimal OfferPrice { get; set; }

    public int RatingCount { get; set; }
    public int RatingRate { get; set; }
    public List<Image> Images { get; set; } = [];
}
