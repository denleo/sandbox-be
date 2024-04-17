using MediatR;
using Sandbox.Utility.Result;
using Sandbox.Wordbook.Application.Dtos;
using Sandbox.Wordbook.Domain.Enums;

namespace Sandbox.Wordbook.Application.Translation.Commands.CreateTranslation;

public record CreateTranslationCommand(
    string Word,
    TranslationLanguage SourceLang,
    TranslationLanguage TargetLang,
    string Translation,
    string PartOfSpeech,
    string? Transcription) : IRequest<Result<TranslationDto>>;