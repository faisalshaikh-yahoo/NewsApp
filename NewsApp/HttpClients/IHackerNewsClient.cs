using NewsApp.Models;

namespace NewsApp.HttpClients
{
    public interface IHackerNewsClient
    {
        Task<List<int>> GetNewStoriesAsync();
        Task<StoryItem> GetStoryItemAsync(int id);
        Task<SearchResult> SearchAsync(string query);
    }
}
