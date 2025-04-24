namespace NewsApp.Cache
{
    public class HackerNewsCache
    {
        private List<int> _newStories = new();
        private readonly object _lock = new();

        public List<int> GetNewStories()
        {
            lock (_lock)
            {
                return new List<int>(_newStories);
            }
        }

        public void SetNewStories(List<int> stories)
        {
            lock (_lock)
            {
                _newStories = stories;
            }
        }
    }
}
