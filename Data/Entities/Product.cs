using bShop.Data.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace bShop.Data.Entities;

public class Product : IUniqueEntity<int>
{
    public int Id { get; set; }
    
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;

    public string ShortDesc { get; set; } = string.Empty;
    public string LongDesc { get; set; } = string.Empty;

    public string Colors { get; set; } = string.Empty;

    public int VendorID { get; set; }
    public virtual Vendor? Vendor { get; set; }

    public int BrandID { get; set; }
    public virtual Brand? Brand { get; set; }

    public int CollectionId { get; set; }
    public virtual Collection? Collection { get; set; }

    public int LabelId { get; set; }
    public virtual Label? Label { get; set; }
    
    public bool Active { get; set; }

    [Column(TypeName = "decimal(16,4)")]
    public decimal CostPrice { get; set; }
    [Column(TypeName = "decimal(16,4)")]
    public decimal SalePrice { get; set; }

    [Column(TypeName = "decimal(16,4)")]
    public decimal OfferPrice { get; set; }
    public DateTime? OfferStartDate{ get; set; }
    public DateTime? OfferEndDate { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public virtual List<Image> Images { get; set; } = [];
    public virtual List<CartItem> CartItems { get; set; } = [];
}
