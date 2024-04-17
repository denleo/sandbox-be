using System.Globalization;
using AutoMapper;
using Humanizer;
using Sandbox.Wordbook.Application.Dtos;
using Sandbox.Wordbook.Domain;

namespace Sandbox.Wordbook.Application.Mappers;

public class TranslationProfile : Profile
{
    public TranslationProfile()
    {
        CreateMap<TranslationResult, TranslationResultDto>();
        CreateMap<Domain.Translation, TranslationDto>()
            .ForMember(x => x.LastViewed,
                opt => opt.MapFrom(x => x.LastViewedAt.Humanize(true, DateTime.UtcNow, CultureInfo.InvariantCulture)));
    }
}