using bShop.Shared.Attributes;
using bShop.Shared.Interfaces;
using System.ComponentModel;

namespace bShop.Server.Data.Entities.Languages;

public class Language: IHasId, IIsDeletableActivable
{
    public int Id { get; set; }

    [ShopRequired]
    [ShopDisplayName(nameof(Resources.Name))]
    public string Name { get; set; } = string.Empty;
    
    [ShopRequired]
    [ShopDisplayName(nameof(Resources.Culture))]
    public string Culture { get; set; } = string.Empty;
    
    [ShopRequired]
    [ShopDisplayName(nameof(Resources.SeoCode))]
    public string SeoCode { get; set; } = string.Empty;
    
    [ShopDisplayName(nameof(Resources.Image))]
    public string Image { get; set; } = string.Empty;
    
    [ShopDisplayName(nameof(Resources.RTL))]
    public bool Rtl { get; set; }

    [DefaultValue(true)]
    [ShopDisplayName(nameof(Resources.IsActive))]
    public bool IsActive { get; set; }
    
    [ShopDisplayName(nameof(Resources.Order))]
    public int DisplayOrder { get; set; }

    public bool IsDeleted { get; set; } 
}
