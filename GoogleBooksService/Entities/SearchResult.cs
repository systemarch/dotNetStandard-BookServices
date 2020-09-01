using System.Collections.Generic;

namespace GoogleBooksService
{
    /// <summary>
    /// Represents the search response body.
    /// See <see href="https://developers.google.com/books/docs/v1/reference/volumes/list#response"/> for reference.
    /// </summary>
    public class SearchResult
    {
        public string Kind { get; set; }
        public int TotalItems { get; set; }
        public IEnumerable<Volume> Items { get; set; }
    }
}
