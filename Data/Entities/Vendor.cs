namespace bShop.Data.Entities;

public class Vendor
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ShopName { get; set; } = string.Empty;
    public string ShopUrl { get; set; } = string.Empty;
    public string ShopPhone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public int CountryId { get; set; } 
    public bool Active { get; set; }
    public string? Logo { get; set; }

    public string? BankName { get; set; } 
    public string? BankCodeIFSC { get; set; } 
    public string? AccountNumber { get; set; }
    public string? AccountHolderName { get; set; }
    public string? PaypalId { get; set; }
    public string? Description { get; set; }

    public List<Product> Products { get; set; } = [];
}
