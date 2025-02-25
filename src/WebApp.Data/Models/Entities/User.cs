namespace WebApp.Data.Models.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string IdentificationNumber { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? Dependency { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }

    // relationships
    public virtual ICollection<UserRole> UserRoles { get; set; } = [];
}