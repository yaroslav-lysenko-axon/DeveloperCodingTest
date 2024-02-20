using CoddingTest.Domain.Models;

namespace CoddingTest.Domain.Services.Abstraction;

public interface IHackerNewsService
{
    Task<IReadOnlyCollection<HackerNewsDto>> Get();
}