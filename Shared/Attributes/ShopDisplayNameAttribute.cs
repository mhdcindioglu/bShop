using System.ComponentModel;
using System.Resources;

namespace bShop.Shared.Attributes;
public class ShopDisplayNameAttribute(string ResourceName) : DisplayNameAttribute
{
    public override string DisplayName =>
        new ResourceManager(typeof(Resources)).GetString(ResourceName) ?? ResourceName;
}
