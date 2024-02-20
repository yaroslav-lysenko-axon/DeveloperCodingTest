using AutoMapper;
using CoddingTest.Domain.Models;

namespace CoddingTest.Domain.Mapping;

public class DomainMappingsProfile : Profile
{
    public DomainMappingsProfile()
    {
        CreateMap<HackerNewsOriginalResponseModel, HackerNewsDto>()
            .ForMember(x => x.Title, y => y.MapFrom(z => z.title))
            .ForMember(x => x.Uri, y => y.MapFrom(z => new Uri(z.url)))
            .ForMember(x => x.PostedBy, y => y.MapFrom(z => z.by))
            .ForMember(x => x.Time, y => y.MapFrom(z => DateTimeOffset.FromUnixTimeSeconds(z.time).DateTime))
            .ForMember(x => x.Score, y => y.MapFrom(z => z.score))
            .ForMember(x => x.CommentCount, y => y.MapFrom(z => z.descendants));
    }
}