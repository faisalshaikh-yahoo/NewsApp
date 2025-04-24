namespace NewsApp.Models
{
    public class HackerNewsException : Exception
    {
        public HackerNewsException(string message) : base(message) { }
        public HackerNewsException(string message, Exception inner) : base(message, inner) { }
    }
}
