using AutoMapper;
using CoddingTest.Domain.Models;
using CodingTest.Application.Models.Responses;

namespace CodingTest.Application.Mapping;

public class ApplicationMappingsProfile : Profile
{
    public ApplicationMappingsProfile()
    {
        // response
        CreateMap<HackerNewsDto, HackerNewsResponseModel>();
    }
}