using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sandbox.Utility.Ordering;
using Sandbox.Utility.Pagination;
using Sandbox.Wordbook.Application;
using Sandbox.Wordbook.Application.Dtos;
using Sandbox.Wordbook.Application.Translation.Commands.CreateTranslation;
using Sandbox.Wordbook.Application.Translation.Commands.RemoveTranslation;
using Sandbox.Wordbook.Application.Translation.Commands.RemoveTranslationResult;
using Sandbox.Wordbook.Application.Translation.Commands.ViewTranslation;
using Sandbox.Wordbook.Application.Translation.Queries.GetTranslationsQuery;
using Sandbox.Wordbook.Domain.Enums;

namespace Sandbox.Wordbook.API.Controllers;

public class TranslationController : CoreController
{
    private readonly ISender _sender;

    public TranslationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        return Ok();
    }

    [HttpGet("translations")]
    public async Task<ActionResult<PagedList<TranslationDto>>> GetTranslations(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string orderBy = nameof(TranslationDto.CreatedAt),
        [FromQuery] Order order = Order.Desc,
        [FromQuery] TranslationLanguage? sourceLang = null,
        [FromQuery] TranslationLanguage? targetLang = null,
        [FromQuery] string? word = null,
        [FromQuery] string? partOfSpeech = null)
    {
        var query = new GetTranslationsQuery(page, pageSize, orderBy, order,
            sourceLang, targetLang, word, partOfSpeech?.Split(','));

        var result = await _sender.Send(query);

        return result.IsSuccess switch
        {
            false when result.Error.Equals(ApplicationErrors.UserNotFound) => NotFound(result.Error),
            _ => result.Value!
        };
    }

    [HttpPost("translations")]
    public async Task<ActionResult<TranslationDto>> CreateTranslation(CreateTranslationCommand payload)
    {
        var result = await _sender.Send(payload);

        return result.IsSuccess switch
        {
            false when result.Error.Equals(ApplicationErrors.UserNotFound) => NotFound(result.Error),
            false when result.Error.Equals(ApplicationErrors.TranslationAlreadyExists) => Conflict(result.Error),
            _ => result.Value!
        };
    }

    [HttpPatch("translations/{translationId:guid}/view")]
    public async Task<ActionResult<TranslationDto>> ViewTranslation([FromRoute] Guid translationId)
    {
        var result = await _sender.Send(new ViewTranslationCommand(translationId));

        return result.IsSuccess switch
        {
            false when result.Error.Equals(ApplicationErrors.TranslationNotFound) => NotFound(result.Error),
            false when result.Error.Equals(ApplicationErrors.UserNotFound) => NotFound(result.Error),
            false when result.Error.Equals(ApplicationErrors.TranslationPermissionFailure) => Forbid(
                result.Error.Description!),
            _ => result.Value!
        };
    }

    [HttpDelete("translations/{translationId:guid}")]
    public async Task<ActionResult<TranslationDto>> RemoveTranslation([FromRoute] Guid translationId)
    {
        var result = await _sender.Send(new RemoveTranslationCommand(translationId));

        return result.IsSuccess switch
        {
            false when result.Error.Equals(ApplicationErrors.TranslationNotFound) => NotFound(result.Error),
            false when result.Error.Equals(ApplicationErrors.UserNotFound) => NotFound(result.Error),
            false when result.Error.Equals(ApplicationErrors.TranslationPermissionFailure) => Forbid(
                result.Error.Description!),
            _ => result.Value!
        };
    }

    [HttpDelete("translations/{translationId:guid}/results/{translationResultId:guid}")]
    public async Task<ActionResult<TranslationDto>> RemoveTranslationResult(
        [FromRoute] Guid translationId,
        [FromRoute] Guid translationResultId)
    {
        var result = await _sender.Send(new RemoveTranslationResultCommand(translationId, translationResultId));

        return result.IsSuccess switch
        {
            false when result.Error.Equals(ApplicationErrors.TranslationNotFound)
                       || result.Error.Equals(ApplicationErrors.TranslationResultNotFound) => NotFound(result.Error),
            _ => result.Value!
        };
    }
}