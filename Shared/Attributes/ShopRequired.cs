using System.ComponentModel.DataAnnotations;

namespace bShop.Shared.Attributes;
public class ShopRequiredAttribute : RequiredAttribute
{
    public ShopRequiredAttribute()
    {
        ErrorMessageResourceName = nameof(Resources.Validation_Required);
        ErrorMessageResourceType = typeof(Resources);
    }
}
