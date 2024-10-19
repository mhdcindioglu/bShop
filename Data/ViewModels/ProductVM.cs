using System.ComponentModel.DataAnnotations.Schema;

namespace bShop.Data.ViewModels;

public class ProductVM
{
    public int Id { get; set; }

    public int CategoryId { get; set; }
    public CategoryVM? Category { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;

    public string ShortDesc { get; set; } = string.Empty;
    public string LongDesc { get; set; } = string.Empty;

    public string? Image { get; set; }
    public int? Version { get; set; }

    public int VendorID { get; set; }
    public VendorVM? Vendor { get; set; }

    public int BrandID { get; set; }
    public BrandVM? Brand { get; set; }

    public int CollectionId { get; set; }
    public CollectionVM? Collection { get; set; }

    public int LabelId { get; set; }
    public LabelVM? Label { get; set; }

    public bool Active { get; set; }

    [Column(TypeName = "decimal(16,4)")]
    public decimal CostPrice { get; set; }
    [Column(TypeName = "decimal(16,4)")]
    public decimal SalePrice { get; set; }

    [Column(TypeName = "decimal(16,4)")]
    public decimal OfferPrice { get; set; }
    public DateTime? OfferStartDate { get; set; }
    public DateTime? OfferEndDate { get; set; }
}
