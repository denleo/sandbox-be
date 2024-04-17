using Sandbox.Wordbook.Domain.Abstractions;

namespace Sandbox.Wordbook.Domain;

public class TranslationResult: AuditableEntity
{
    public required string Translation { get; set; }
    public required string PartOfSpeech { get; set; }
    public string? Transcription { get; set; }

    public Guid TranslationId { get; set; }
}