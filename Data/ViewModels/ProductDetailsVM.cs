namespace bShop.Data.ViewModels;

public class ProductDetailsVM
{
    public ProductDetailsVM()
    {
    }
    public int Id { get; set; }
    public List<ProductCardLabelVM> Labels { get; set; } = [];
    
    public int CategoryId { get; set; }
    public CategoryVM? Category { get; set; }
    
    public bool IsInCart { get; set; }
    public bool IsInWishList { get; set; }
    public bool ShowRating { get; set; }
    public string[] Colors { get; set; } = [];
    public string Link => $"/Product/{Id}/{Slug}";
    public string Image { get; set; } = string.Empty;
    public int Version { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;

    public int BrandID { get; set; }
    public string BrandName { get; set; } = string.Empty;

    public int VendorID { get; set; }
    public string VendorName { get; set; } = string.Empty;

    public decimal OldPrice { get; set; }
    public decimal Price { get; set; }

    public int RatingCount { get; set; }
    public int RatingRate { get; set; }
}
