using AutoMapper;
using CoddingTest.Domain.Services.Abstraction;
using CodingTest.Application.Models.Commands;
using CodingTest.Application.Models.Responses;
using MediatR;

namespace CodingTest.Application.Handlers;

public class HackerNewsHandler : IRequestHandler<GetHackerNewsCommand, IReadOnlyCollection<HackerNewsResponseModel>>
{
    private readonly IHackerNewsService _hackerNewsService;
    private readonly IMapper _mapper;

    public HackerNewsHandler(
        IHackerNewsService hackerNewsService,
        IMapper mapper)
    {
        _hackerNewsService = hackerNewsService;
        _mapper = mapper;
    }
    public async Task<IReadOnlyCollection<HackerNewsResponseModel>> Handle(
        GetHackerNewsCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _hackerNewsService.Get();
        return _mapper.Map<List<HackerNewsResponseModel>>(response);
    }
}