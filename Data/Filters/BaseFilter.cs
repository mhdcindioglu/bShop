using bShop.Data.Enums;

namespace bShop.Data.Filters;

public class BaseFilter
{
    public int CurrentPage { get; set; } = 1;
    public Showing Showing { get; set; } = Showing.Showing_12;
    public Sort Sort { get; set; } = Sort.Default;
}
