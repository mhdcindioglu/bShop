namespace bShop.Data.Entities;

public class Label
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = [];
}
