using Sandbox.Wordbook.Domain.Enums;

namespace Sandbox.Wordbook.Application.Dtos;

public class TranslationDto
{
    public Guid Id { get; set; }
    public string? Word { get; set; }
    public TranslationLanguage SourceLang { get; set; }
    public TranslationLanguage TargetLang { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? LastViewed { get; set; }
    public ICollection<TranslationResultDto>? TranslationResults { get; set; }
}