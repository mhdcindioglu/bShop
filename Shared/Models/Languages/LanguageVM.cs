namespace bShop.Shared.Models.Languages;
public class LanguageVM
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Culture { get; set; } = string.Empty;
    public string SeoCode { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public bool Rtl { get; set; }
}
