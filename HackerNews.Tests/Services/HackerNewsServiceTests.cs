using Moq;
using NewsApp.Cache;
using NewsApp.HttpClients;
using NewsApp.Services;

public class HackerNewsServiceTests
{
    private readonly Mock<IHackerNewsClient> _clientMock;
    private readonly HackerNewsCache _cache;
    private readonly HackerNewsService _service;

    public HackerNewsServiceTests()
    {
        _clientMock = new Mock<IHackerNewsClient>();
        _cache = new HackerNewsCache();
        _service = new HackerNewsService(_clientMock.Object, _cache);
    }

    [Fact]
    public async Task GetNewStoriesAsync_ReturnsFromCache_IfAvailable()
    {
        // Arrange
        var expected = new List<int> { 1, 2, 3 };
        _cache.SetNewStories(expected);

        // Act
        var result = await _service.GetNewStoriesAsync();

        // Assert
        Assert.Equal(expected, result);
        _clientMock.Verify(c => c.GetNewStoriesAsync(), Times.Never);
    }

    [Fact]
    public async Task GetNewStoriesAsync_FetchesFromClient_IfCacheEmpty()
    {
        // Arrange
        var expected = new List<int> { 1, 2 };
        _clientMock.Setup(c => c.GetNewStoriesAsync()).ReturnsAsync(expected);

        // Act
        var result = await _service.GetNewStoriesAsync();

        // Assert
        Assert.Equal(expected, result);
        _clientMock.Verify(c => c.GetNewStoriesAsync(), Times.Once);
    }
}
