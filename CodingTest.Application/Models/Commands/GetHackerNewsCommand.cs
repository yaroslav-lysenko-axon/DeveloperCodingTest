using CodingTest.Application.Models.Responses;
using MediatR;

namespace CodingTest.Application.Models.Commands;

public class GetHackerNewsCommand : IRequest<IReadOnlyCollection<HackerNewsResponseModel>>
{
}