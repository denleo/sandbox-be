using MediatR;
using Sandbox.Utility.Ordering;
using Sandbox.Utility.Pagination;
using Sandbox.Utility.Result;
using Sandbox.Wordbook.Application.Dtos;
using Sandbox.Wordbook.Domain.Enums;

namespace Sandbox.Wordbook.Application.Translation.Queries.GetTranslationsQuery;

public record GetTranslationsQuery(
    int Page,
    int PageSize,
    string OrderBy,
    Order Order,
    TranslationLanguage? SourceLang = null,
    TranslationLanguage? TargetLang = null,
    string? Word = null,
    string[]? PartOfSpeech = null)
    : IRequest<Result<PagedList<TranslationDto>>>;