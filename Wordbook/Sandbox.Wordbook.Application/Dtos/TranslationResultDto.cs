namespace Sandbox.Wordbook.Application.Dtos;

public class TranslationResultDto
{
    public Guid Id { get; set; }
    public string? Translation { get; set; }
    public string? PartOfSpeech { get; set; }
    public string? Transcription { get; set; }
}