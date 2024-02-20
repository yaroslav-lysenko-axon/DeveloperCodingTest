namespace CodingTest.Application.Models.Responses;

public class HackerNewsResponseModel
{
    public string Title { get; set; }
    public Uri Uri { get; set; }
    public string PostedBy { get; set; }
    public DateTime Time { get; set; }
    public int Score { get; set; }
    public int CommentCount { get; set; }
}