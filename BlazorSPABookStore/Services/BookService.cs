using BlazorSPABookStore.Models;
using System.Text.Json;
using System.Text;
using BlazorSPABookStore.Interfaces;
using System.Net.Http.Json;

namespace BlazorSPABookStore.Services
{
    public class BookService : IBookService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUri;

        public BookService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _baseUri = _configuration.GetSection("BookStoreApi:Url").Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetFromJsonAsync<IEnumerable<Book>>($"{_baseUri}api/books");

            return response;
        }

        public async Task<Book> GetById(int bookId)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync($"{_baseUri}api/books/{bookId}");

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Book>
                    (await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }

            return null;
        }

        public async Task<Book> Add(Book book)
        {
            var httpClient = _httpClientFactory.CreateClient(_baseUri);

            var bookJson = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{_baseUri}api/books", bookJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Book>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task<bool> Update(Book book)
        {
            var httpClient = _httpClientFactory.CreateClient(_baseUri);

            var bookJson = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync($"{_baseUri}api/books/{book.Id}", bookJson);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> Delete(int bookId)
        {
            var httpClient = _httpClientFactory.CreateClient(_baseUri);

            var result = await httpClient.DeleteAsync($"{_baseUri}api/books/{bookId}");

            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
    }
}