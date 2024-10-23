using bShop.Data.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace bShop.Data.Entities;

public class CartItem : IUniqueEntity<int>
{
    public int Id { get; set; }
    
    public int CartId { get; set; }
    public Cart? Cart { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public string? Color { get; set; }

    [Column(TypeName = "decimal(16,4)")]
    public decimal Quantity { get; set; }

    [Column(TypeName = "decimal(16,4)")]
    public decimal Price { get; set; }

    public int? Rate { get; set; }
}