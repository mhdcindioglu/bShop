using bShop.Shared.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bShop.Server.Data.Entities.Users;

public class ShopRole : IdentityRole<int>, IHasId, IIsDeletableActivable
{
    public ShopRole() { }
    public ShopRole(string name) { Name = name; }


    [DefaultValue(true)]
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;

    [MaxLength(64)]
    public override string? ConcurrencyStamp { get; set; }

    public virtual List<ShopUser> Users { get; set; } = [];

}
