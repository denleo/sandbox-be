using MediatR;
using Sandbox.Utility.Result;
using Sandbox.Wordbook.Application.Dtos;

namespace Sandbox.Wordbook.Application.Translation.Commands.RemoveTranslationResult;

public record RemoveTranslationResultCommand(Guid TranslationId, Guid TranslationResultId)
    : IRequest<Result<TranslationDto>>;