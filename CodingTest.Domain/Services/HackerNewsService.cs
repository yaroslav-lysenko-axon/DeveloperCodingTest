using System.Net.Http.Json;
using System.Text.Json;
using AutoMapper;
using CoddingTest.Domain.Models;
using CoddingTest.Domain.Services.Abstraction;
using Newtonsoft.Json;

namespace CoddingTest.Domain.Services;

public class HackerNewsService(
    IMapper mapper,
    IHttpClientFactory httpClientFactory) : IHackerNewsService
{
    private const string BestStoriesUrl = "https://hacker-news.firebaseio.com/v0/beststories.json";
    private const string BestStoriesById = "https://hacker-news.firebaseio.com/v0/item/";
    public async Task<IReadOnlyCollection<HackerNewsDto>> Get()
    {
       var ids = await GetBestStoriesIds();

       if (ids == null || ids.Count == 0)
           return Array.Empty<HackerNewsDto>();
       
       var tasks = ids.Select(GetBestStory);
       var bestStories = await Task.WhenAll(tasks);

        var bestStoriesDto = mapper.Map<List<HackerNewsDto>>(bestStories);
        
        return bestStoriesDto.OrderByDescending(hackerNews => hackerNews.Score).ToList();
    }
    
    private async Task<HashSet<long>?> GetBestStoriesIds()
    {
        using var handler = new HttpClientHandler();
        using var client = new HttpClient(handler);

        client.BaseAddress = new Uri(BestStoriesUrl);
        client.DefaultRequestHeaders.Clear();

        var httpResponseMessage = await client.GetAsync(string.Empty).ConfigureAwait(false);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            string response = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<HashSet<long>>(response);
        }

        return null;
    }

    private async Task<HackerNewsOriginalResponseModel?> GetBestStory(long id)
    {
        using HttpClient client = httpClientFactory.CreateClient();

        return await client.GetFromJsonAsync<HackerNewsOriginalResponseModel>(
            $"{BestStoriesById}{id}.json",
            new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }
}