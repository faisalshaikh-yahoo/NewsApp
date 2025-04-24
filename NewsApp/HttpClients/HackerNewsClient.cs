using NewsApp.Models;

namespace NewsApp.HttpClients
{
    public class HackerNewsClient : IHackerNewsClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HackerNewsClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<int>> GetNewStoriesAsync()
        {
            var client = _httpClientFactory.CreateClient("HackerNews");

            try
            {
                return await client.GetFromJsonAsync<List<int>>("newstories.json?print=pretty");
            }
            catch (Exception ex)
            {
                throw new HackerNewsException("Error fetching new stories", ex);
            }
        }

        public async Task<StoryItem> GetStoryItemAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("HackerNews");

            try
            {
                return await client.GetFromJsonAsync<StoryItem>($"item/{id}.json?print=pretty");
            }
            catch (Exception ex)
            {
                throw new HackerNewsException($"Error fetching story with ID {id}", ex);
            }
        }

        public async Task<SearchResult> SearchAsync(string query)
        {
            var client = _httpClientFactory.CreateClient("HackerNewsSearch");

            try
            {
                return await client.GetFromJsonAsync<SearchResult>($"search?query={query}");
            }
            catch (Exception ex)
            {
                throw new HackerNewsException("Error searching stories", ex);
            }
        }
    }
}
