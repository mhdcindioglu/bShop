namespace bShop.Data.ViewModels;

public class LabelVM
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public List<ProductVM> Products { get; set; } = [];
}