namespace Sandbox.Wordbook.Domain.Abstractions;

public abstract class AuditableEntity: Entity<Guid>
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}