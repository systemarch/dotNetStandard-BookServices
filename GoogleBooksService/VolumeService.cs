using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoogleBooksService
{
    public class VolumeService : Service
    {
        /// <summary>
        /// Returns a list of Google Books publishers.
        /// Note that this list is not complete, and the values are hard-coded.
        /// </summary>
        /// <returns>A list of publishers.</returns>
        public static List<string> GetPublishers()
        {
            return new List<string>()
            {
                "A&C Black",
                "Anchor",
                "Ballantine Books",
                "Bantam",
                "Booktango",
                "Courier Corporation",
                "Del Rey",
                "Doubleday",
                "Fawcett",
                "Gollancz",
                "Good Press",
                "Good Year Books",
                "Harper Collins",
                "Houghton Mifflin Harcourt",
                "Knopf Books for Young Readers",
                "Little, Brown",
                "Macmillan",
                "MDP Publishing",
                "Open Road Media",
                "Pottermore Publishing",
                "Random House",
                "Robinson",
                "Simon and Schuster",
                "Spectra",
                "Tor Books",
                "U of Minnesota Press",
                "Wildside Press LLC",
            };
        }

        /// <summary>
        /// Returns a list of Google Books categories.
        /// Note that this list is not complete, and the values are hard-coded.
        /// </summary>
        /// <returns>A list of categories.</returns>
        public static List<string> GetCategories()
        {
            return new List<string>()
            {
                "Art",
                "Biography & Autobiography",
                "Comics & Graphic Novels",
                "Cooking",
                "Drama",
                "Education",
                "Electricity",
                "Fiction",
                "History",
                "Humor",
                "Juvenile Fiction",
                "Pamphlets",
                "Philosophy",
                "Science",
                "Self-Help",
                "Young Adult Fiction",
            };
        }


        /// <summary>
        /// Sends a search request. Gets <paramref name="maxResults"/> items (up to 40),
        ///     starting from the <paramref name="startIndex"/> position in the search list.
        /// See <see href="https://developers.google.com/books/docs/v1/using#PerformingSearch"/> for reference.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <param name="startIndex"></param>
        /// <param name="maxResults"></param>
        /// <returns>A task containing the <see cref="HttpResponseMessage"/> object.</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<HttpResponseMessage> SendSearchRequestAsync(string query,
            int startIndex = 0, int maxResults = 40)
        {
            try
            {
                return await SendGetRequestAsync(
                $"volumes?q={query}&filter=ebooks&printType=books&startIndex={startIndex}&maxResults={maxResults}");
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Cannot access the Google Books API server.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Performs a volumes search with the given query.
        /// Returns up to <paramref name="maxBooks"/> items.
        /// The exact quantity of items can vary and cannot be guaranteed.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <returns>A task containing the <see cref="SearchResult"/> object.</returns>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<SearchResult> SearchAsync(string query, int maxBooks = 40)
        {
            int startIndex = 0;
            const int BooksPerRequest = 40;
            var itemsList = new List<Volume>();

            var response = await SendSearchRequestAsync(query);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = Serialization.DeserializeJson<SearchResult>(responseContent);
                    if (result.Items == null)
                    {
                        throw new ArgumentNullException("Response contains empty list");
                    }

                    itemsList.AddRange(result.Items);

                    while (startIndex < maxBooks - BooksPerRequest || startIndex < result.TotalItems - BooksPerRequest)
                    {
                        startIndex += BooksPerRequest;
                        response = await SendSearchRequestAsync(query, startIndex);
                        if (response.IsSuccessStatusCode)
                        {
                            responseContent = await response.Content.ReadAsStringAsync();
                            var nextResult = Serialization.DeserializeJson<SearchResult>(responseContent);

                            try
                            {
                                itemsList.AddRange(nextResult.Items);
                            }
                            catch (ArgumentNullException)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    result.Items = itemsList.Take(maxBooks).AsEnumerable();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Performs a volumes search by the author's name.
        /// Returns up to <paramref name="maxBooks"/> items.
        /// The exact quantity of items can vary and cannot be guaranteed.
        /// </summary>
        /// <param name="authorName"></param>
        /// <param name="maxBooks"></param>
        /// <returns>A task containing the <see cref="SearchResult"/> object.</returns>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<SearchResult> SearchByAuthorAsync(string authorName, int maxBooks = 40)
        {
            try
            {
                return await SearchAsync($"inauthor:{string.Join("+", authorName.Split(' '))}", maxBooks);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
