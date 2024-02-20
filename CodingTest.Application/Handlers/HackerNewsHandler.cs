using AutoMapper;
using CoddingTest.Domain.Services.Abstraction;
using CodingTest.Application.Models.Commands;
using CodingTest.Application.Models.Responses;
using MediatR;

namespace CodingTest.Application.Handlers;

public abstract class HackerNewsHandler(
    IHackerNewsService hackerNewsService,
    IMapper mapper) : IRequestHandler<GetHackerNewsCommand, IReadOnlyCollection<HackerNewsResponseModel>>
{
    public async Task<IReadOnlyCollection<HackerNewsResponseModel>> Handle(
        GetHackerNewsCommand request,
        CancellationToken cancellationToken)
    {
        var response = await hackerNewsService.Get();
        return mapper.Map<List<HackerNewsResponseModel>>(response);
    }
}