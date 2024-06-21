using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bShop.Shared.Models.Products;
public class ProductCardVM
{
    public ProductCardVM()
    {
        RatingCount = Random.Shared.Next(999);
        RatingRate = Random.Shared.Next(100);
    }
    public int ID { get; set; }
    public List<ProductCardLabelVM> Labels { get; set; } = [];
    public bool IsInCart { get; set; }
    public bool IsInWishList { get; set; }
    public bool ShowRating { get; set; }
    public string[] Colors { get; set; } = [];
    public string Link { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public int Version { get; set; }
    public string Name { get; set; } = string.Empty;

    public int BrandID { get; set; }
    public string BrandName { get; set; } = string.Empty;

    public int VendorID { get; set; }
    public string VendorName { get; set; } = string.Empty;

    public decimal OldPrice { get; set; }
    public decimal Price { get; set; }

    public int RatingCount { get; set; }
    public int RatingRate { get; set; }
}

public class ProductCardLabelVM
{
    public string Text { get; set; } = string.Empty;
    public string FColor { get; set; } = string.Empty;
    public string BColor { get; set; } = string.Empty;
}