namespace CoddingTest.Domain.Models;

public class HackerNewsOriginalResponseModel
{
    public string title { get; set; }
    public string url { get; set; }
    public string by { get; set; }
    public long time { get; set; }
    public int score { get; set; }
    public int descendants { get; set; }
}