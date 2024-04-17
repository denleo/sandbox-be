using AutoMapper;
using MediatR;
using Sandbox.Contracts.Auth;
using Sandbox.Utility.Result;
using Sandbox.Wordbook.Application.Dtos;
using Sandbox.Wordbook.Domain.Abstractions;

namespace Sandbox.Wordbook.Application.Translation.Commands.RemoveTranslation;

public class RemoveTranslationHandler : IRequestHandler<RemoveTranslationCommand, Result<TranslationDto>>
{
    private readonly IAuthenticatedContext _authenticatedContext;
    private readonly IMapper _mapper;
    private readonly ITranslationRepository _translationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWordbookUserRepository _userRepository;

    public RemoveTranslationHandler(
        ITranslationRepository translationRepository,
        IWordbookUserRepository userRepository,
        IAuthenticatedContext authenticatedContext,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _translationRepository = translationRepository;
        _userRepository = userRepository;
        _authenticatedContext = authenticatedContext;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TranslationDto>> Handle(
        RemoveTranslationCommand request,
        CancellationToken cancellationToken)
    {
        var translation = await _translationRepository.GetByIdAsync(request.TranslationId, cancellationToken);
        if (translation is null) return Result<TranslationDto>.Failure(ApplicationErrors.TranslationNotFound);

        var user = await _userRepository.GetUserByFirebaseIdAsync(_authenticatedContext.FirebaseId, cancellationToken);
        if (user is null) return Result<TranslationDto>.Failure(ApplicationErrors.UserNotFound);

        if (translation.UserId != user.Id)
            return Result<TranslationDto>.Failure(ApplicationErrors.TranslationPermissionFailure);

        _translationRepository.DeleteTranslation(translation);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<TranslationDto>.Success(_mapper.Map<TranslationDto>(translation));
    }
}