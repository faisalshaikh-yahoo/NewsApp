using NewsApp.Models;

namespace NewsApp.Services
{
    public interface IHackerNewsService
    {
        Task<List<int>> GetNewStoriesAsync();
        Task<StoryItem> GetStoryItemAsync(int id);
        Task<SearchResult> SearchAsync(string query);
    }
}
