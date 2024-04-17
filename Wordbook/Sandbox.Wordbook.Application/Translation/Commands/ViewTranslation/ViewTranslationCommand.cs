using MediatR;
using Sandbox.Utility.Result;
using Sandbox.Wordbook.Application.Dtos;

namespace Sandbox.Wordbook.Application.Translation.Commands.ViewTranslation;

public record ViewTranslationCommand(Guid TranslationId) : IRequest<Result<TranslationDto>>;