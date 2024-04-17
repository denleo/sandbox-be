using Sandbox.Wordbook.Domain.Abstractions;
using Sandbox.Wordbook.Domain.Enums;

namespace Sandbox.Wordbook.Domain;

public class Translation : AuditableEntity
{
    public required TranslationLanguage SourceLang { get; set; }
    public required TranslationLanguage TargetLang { get; set; }
    public required string Word { get; set; }
    public required DateTime LastViewedAt { get; set; }

    public required Guid UserId { get; set; }
    public List<TranslationResult> TranslationResults { get; set; } = [];
}