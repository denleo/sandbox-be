using MediatR;
using Sandbox.Utility.Result;
using Sandbox.Wordbook.Application.Dtos;

namespace Sandbox.Wordbook.Application.Translation.Commands.RemoveTranslation;

public record RemoveTranslationCommand(Guid TranslationId) : IRequest<Result<TranslationDto>>;