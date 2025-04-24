using NewsApp.Cache;
using NewsApp.HttpClients;
using NewsApp.Models;

namespace NewsApp.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly IHackerNewsClient _client;
        private readonly HackerNewsCache _cache;

        public HackerNewsService(IHackerNewsClient client, HackerNewsCache cache)
        {
            _client = client;
            _cache = cache;
        }

        public async Task<List<int>> GetNewStoriesAsync()
        {
            var cached = _cache.GetNewStories();
            if (cached.Any()) return cached;

            var stories = await _client.GetNewStoriesAsync();
            _cache.SetNewStories(stories);
            return stories;
        }

        public async Task<StoryItem> GetStoryItemAsync(int id) =>
            await _client.GetStoryItemAsync(id);

        public async Task<SearchResult> SearchAsync(string query) =>
            await _client.SearchAsync(query);
    }
}
