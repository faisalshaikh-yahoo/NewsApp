using Microsoft.AspNetCore.Mvc;
using NewsApp.Models;
using NewsApp.Services;

namespace NewsApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HackerNewsController : ControllerBase
{
    private readonly IHackerNewsService _service;

    public HackerNewsController(IHackerNewsService service)
    {
        _service = service;
    }

    [HttpGet("newstories")]
    public async Task<IActionResult> GetNewStories()
    {
        try
        {
            var stories = await _service.GetNewStoriesAsync();
            return Ok(stories);
        }
        catch (HackerNewsException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("item/{id}")]
    public async Task<IActionResult> GetStory(int id)
    {
        try
        {
            var story = await _service.GetStoryItemAsync(id);
            return Ok(story);
        }
        catch (HackerNewsException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string query)
    {
        try
        {
            var result = await _service.SearchAsync(query);
            return Ok(result);
        }
        catch (HackerNewsException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}

