using bShop.Shared.Attributes;
using bShop.Shared.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bShop.Server.Data.Entities.Users;

public partial class ShopUser : IdentityUser<int>, IHasId, IIsDeletableActivable
{
    [ShopRequired, MaxLength(64)]
    [ShopDisplayName(nameof(Resources.FullName))]
    public string FullName { get; set; } = string.Empty;

    [ShopDisplayName(nameof(Resources.PasswordUpdated))]
    public bool PasswordUpdated { get; set; } = false;

    [DefaultValue(true)]
    [ShopDisplayName(nameof(Resources.IsActive))]
    public bool IsActive { get; set; } = true;
    
    public bool IsDeleted { get; set; } = false;

    [MaxLength(512)]
    [ShopDisplayName(nameof(Resources.Address))]
    public string? Address { get; set; }

    [MaxLength(64)]
    [ShopDisplayName(nameof(Resources.Image))]
    public string? Image { get; set; }
    public int? Version { get; set; }

    [MaxLength(128)]
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }

    [MaxLength(512)]
    public override string? PasswordHash { get; set; }

    [MaxLength(512)]
    public override string? SecurityStamp { get; set; }

    [MaxLength(512)]
    public override string? ConcurrencyStamp { get; set; }

    [MaxLength(64)]
    [ShopDisplayName(nameof(Resources.PhoneNumber))]
    public override string? PhoneNumber { get; set; }

    public virtual List<ShopRole> Roles { get; set; } = [];
}
