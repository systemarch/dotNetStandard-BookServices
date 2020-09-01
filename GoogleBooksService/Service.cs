using System.Net.Http;
using System.Threading.Tasks;

namespace GoogleBooksService
{
    public class Service
    {
        /// <summary>
        /// Sends the GET request to the Google Books API v1 with the given query.
        /// See <see href="https://developers.google.com/books/docs/v1/using"/> for reference.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>A task containing the <see cref="HttpResponseMessage"/> object.</returns>
        public async Task<HttpResponseMessage> SendGetRequestAsync(string query)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://www.googleapis.com/books/v1/" + query);

            return await httpClient.SendAsync(request);
        }
    }
}
