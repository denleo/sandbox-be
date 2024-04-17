using AutoMapper;
using MediatR;
using Sandbox.Contracts.Auth;
using Sandbox.Utility.Result;
using Sandbox.Wordbook.Application.Dtos;
using Sandbox.Wordbook.Domain;
using Sandbox.Wordbook.Domain.Abstractions;

namespace Sandbox.Wordbook.Application.Translation.Commands.CreateTranslation;

public class CreateTranslationHandler : IRequestHandler<CreateTranslationCommand, Result<TranslationDto>>
{
    private readonly IAuthenticatedContext _authenticatedContext;
    private readonly IMapper _mapper;
    private readonly ITranslationRepository _translationRepository;
    private readonly IUnitOfWork _uow;
    private readonly IWordbookUserRepository _userRepository;

    public CreateTranslationHandler(
        IAuthenticatedContext authenticatedContext,
        IWordbookUserRepository userRepository,
        ITranslationRepository translationRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _authenticatedContext = authenticatedContext;
        _userRepository = userRepository;
        _translationRepository = translationRepository;
        _uow = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<TranslationDto>> Handle(
        CreateTranslationCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .GetUserByFirebaseIdAsync(_authenticatedContext.FirebaseId, cancellationToken);

        if (user is null) return Result<TranslationDto>.Failure(ApplicationErrors.UserNotFound);

        var translation = await _translationRepository
            .GetByWordAsync(user.Id, request.Word, request.SourceLang, request.TargetLang, cancellationToken);

        if (translation is not null)
        {
            if (translation.TranslationResults
                .Any(x => x.PartOfSpeech == request.PartOfSpeech && x.Translation == request.Translation))
                return Result<TranslationDto>.Failure(ApplicationErrors.TranslationAlreadyExists);

            translation.TranslationResults.Add(new TranslationResult
            {
                PartOfSpeech = request.PartOfSpeech,
                Translation = request.Translation,
                Transcription = request.Transcription
            });
        }
        else
        {
            translation = new Domain.Translation
            {
                UserId = user.Id,
                Word = request.Word,
                SourceLang = request.SourceLang,
                TargetLang = request.TargetLang,
                LastViewedAt = DateTime.UtcNow,
                TranslationResults =
                [
                    new TranslationResult
                    {
                        PartOfSpeech = request.PartOfSpeech,
                        Translation = request.Translation,
                        Transcription = request.Transcription
                    }
                ]
            };

            _translationRepository.CreateTranslation(translation);
        }

        await _uow.SaveChangesAsync(cancellationToken);

        var translationDto = _mapper.Map<TranslationDto>(translation);
        return Result<TranslationDto>.Success(translationDto);
    }
}