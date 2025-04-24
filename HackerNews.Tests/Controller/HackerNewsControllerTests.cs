using Microsoft.AspNetCore.Mvc;
using Moq;
using NewsApp.Controllers;
using NewsApp.Models;
using NewsApp.Services;

public class HackerNewsControllerTests
{
    private readonly HackerNewsController _controller;
    private readonly Mock<IHackerNewsService> _serviceMock;

    public HackerNewsControllerTests()
    {
        _serviceMock = new Mock<IHackerNewsService>();
        _controller = new HackerNewsController(_serviceMock.Object);
    }

    [Fact]
    public async Task GetNewStories_ReturnsOkWithData()
    {
        var list = new List<int> { 1, 2, 3 };
        _serviceMock.Setup(s => s.GetNewStoriesAsync()).ReturnsAsync(list);

        var result = await _controller.GetNewStories() as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(list, result.Value);
    }

    [Fact]
    public async Task GetStory_Returns500_OnException()
    {
        _serviceMock.Setup(s => s.GetStoryItemAsync(It.IsAny<int>()))
                    .ThrowsAsync(new HackerNewsException("Failed"));

        var result = await _controller.GetStory(999) as ObjectResult;

        Assert.Equal(500, result.StatusCode);
        Assert.Equal("Failed", result.Value);
    }
}
