using System.Security.Claims;

namespace bShop.Client.Models;

public class CurrnetUserModel
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public ClaimsPrincipal User { get; set; } = new ClaimsPrincipal();
    public bool IsInRole(string role) => User.IsInRole(role);
}
