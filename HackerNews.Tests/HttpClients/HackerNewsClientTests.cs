using Moq;
using System.Net;
using NewsApp.HttpClients;
using Moq.Protected;

public class HackerNewsClientTests
{
    private HttpClient CreateHttpClientWithResponse(string jsonResponse, string baseAddress)
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse),
            });

        return new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri(baseAddress)
        };
    }

    private IHttpClientFactory SetupHttpClientFactory(Dictionary<string, HttpClient> namedClients)
    {
        var factoryMock = new Mock<IHttpClientFactory>();

        foreach (var kvp in namedClients)
        {
            factoryMock.Setup(x => x.CreateClient(kvp.Key)).Returns(kvp.Value);
        }

        return factoryMock.Object;
    }

    [Fact]
    public async Task GetNewStoriesAsync_ReturnsListOfIds()
    {
        // Arrange
        var json = "[1,2,3]";
        var httpClient = CreateHttpClientWithResponse(json, "https://hacker-news.firebaseio.com/v0/");
        var factory = SetupHttpClientFactory(new Dictionary<string, HttpClient>
        {
            { "HackerNews", httpClient }
        });

        var client = new HackerNewsClient(factory);

        // Act
        var result = await client.GetNewStoriesAsync();

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Contains(1, result);
    }

    [Fact]
    public async Task GetStoryItemAsync_ReturnsStory()
    {
        // Arrange
        var json = "{\"id\": 123, \"title\": \"Test Story\"}";
        var httpClient = CreateHttpClientWithResponse(json, "https://hacker-news.firebaseio.com/v0/");
        var factory = SetupHttpClientFactory(new Dictionary<string, HttpClient>
        {
            { "HackerNews", httpClient }
        });

        var client = new HackerNewsClient(factory);

        // Act
        var result = await client.GetStoryItemAsync(123);

        // Assert
        Assert.Equal(123, result.Id);
        Assert.Equal("Test Story", result.Title);
    }

    [Fact]
    public async Task SearchAsync_ReturnsSearchResult()
    {
        // Arrange
        var json = "{\"hits\": [], \"nbHits\": 0}";
        var httpClient = CreateHttpClientWithResponse(json, "http://hn.algolia.com/api/v1/");
        var factory = SetupHttpClientFactory(new Dictionary<string, HttpClient>
        {
            { "HackerNewsSearch", httpClient }
        });

        var client = new HackerNewsClient(factory);

        // Act
        var result = await client.SearchAsync("test");

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Hits);
    }
}
