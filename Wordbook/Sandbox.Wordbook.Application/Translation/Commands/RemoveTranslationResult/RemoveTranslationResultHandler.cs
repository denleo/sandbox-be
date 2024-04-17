using AutoMapper;
using MediatR;
using Sandbox.Utility.Result;
using Sandbox.Wordbook.Application.Dtos;
using Sandbox.Wordbook.Domain.Abstractions;

namespace Sandbox.Wordbook.Application.Translation.Commands.RemoveTranslationResult;

public class RemoveTranslationResultHandler : IRequestHandler<RemoveTranslationResultCommand, Result<TranslationDto>>
{
    private readonly IMapper _mapper;
    private readonly ITranslationRepository _translationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveTranslationResultHandler(
        ITranslationRepository translationRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _translationRepository = translationRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TranslationDto>> Handle(
        RemoveTranslationResultCommand request,
        CancellationToken cancellationToken)
    {
        var translation = await _translationRepository.GetByIdAsync(request.TranslationId, cancellationToken);
        if (translation is null) return Result<TranslationDto>.Failure(ApplicationErrors.TranslationNotFound);

        var item = translation.TranslationResults.Find(x => x.Id == request.TranslationResultId);
        if (item is null) return Result<TranslationDto>.Failure(ApplicationErrors.TranslationResultNotFound);

        translation.TranslationResults.Remove(item);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<TranslationDto>.Success(_mapper.Map<TranslationDto>(translation));
    }
}