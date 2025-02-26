using WebApp.Common.Constants;

namespace WebApp.Data.Models.Entities;

public class BaseEntity
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string CreatedBy { get; set; } = GlobalConstants.SYSTEM;
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; } // soft delete
}