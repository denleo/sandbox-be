using Sandbox.Wordbook.Domain.Abstractions;

namespace Sandbox.Wordbook.Domain;

public class WordbookUser : AuditableEntity
{
    public required string FirebaseId { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }

    public List<Translation> Translations { get; set; } = [];
}