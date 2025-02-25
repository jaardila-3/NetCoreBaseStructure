namespace WebApp.Data.Models.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // relationships
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}