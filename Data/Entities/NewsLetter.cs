using bShop.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace bShop.Data.Entities;

public class NewsLetter : IUniqueEntity<int>
{
    public int Id { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    public bool Active { get; set; } = true;

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}
