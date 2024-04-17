using AutoMapper;
using MediatR;
using Sandbox.Contracts.Auth;
using Sandbox.Utility.Ordering;
using Sandbox.Utility.Pagination;
using Sandbox.Utility.Result;
using Sandbox.Wordbook.Application.Dtos;
using Sandbox.Wordbook.Application.Mappers;
using Sandbox.Wordbook.Domain.Abstractions;

namespace Sandbox.Wordbook.Application.Translation.Queries.GetTranslationsQuery;

public class GetTranslationsQueryHandler : IRequestHandler<GetTranslationsQuery, Result<PagedList<TranslationDto>>>
{
    private readonly IAuthenticatedContext _authenticatedContext;
    private readonly IMapper _mapper;
    private readonly ITranslationRepository _translationRepository;
    private readonly IWordbookUserRepository _userRepository;

    public GetTranslationsQueryHandler(
        IAuthenticatedContext authenticatedContext,
        IWordbookUserRepository userRepository,
        ITranslationRepository translationRepository,
        IMapper mapper)
    {
        _authenticatedContext = authenticatedContext;
        _userRepository = userRepository;
        _translationRepository = translationRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedList<TranslationDto>>> Handle(
        GetTranslationsQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .GetUserByFirebaseIdAsync(_authenticatedContext.FirebaseId, cancellationToken);

        if (user is null) return Result<PagedList<TranslationDto>>.Failure(ApplicationErrors.UserNotFound);

        var pagedTranslations = await _translationRepository
            .GetUserTranslationsAsync(
                user.Id,
                new PaginationOptions(request.Page, request.PageSize),
                new OrderOptions(request.OrderBy, request.Order),
                request.SourceLang, request.TargetLang, request.Word, request.PartOfSpeech, cancellationToken);

        var result = pagedTranslations.MapTo<Domain.Translation, TranslationDto>(_mapper);
        return Result<PagedList<TranslationDto>>.Success(result);
    }
}