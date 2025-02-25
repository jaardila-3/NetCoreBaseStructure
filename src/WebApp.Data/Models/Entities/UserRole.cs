namespace WebApp.Data.Models.Entities;

public class UserRole : BaseEntity
{
    public Ulid UserId { get; set; }
    public Ulid RoleId { get; set; }

    // relationships
    public virtual User User { get; set; } = null!;
    public virtual Role Role { get; set; } = null!;
}