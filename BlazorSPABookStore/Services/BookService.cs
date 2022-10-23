using BlazorSPABookStore.Models;
using System.Text.Json;
using System.Text;
using BlazorSPABookStore.Interfaces;

namespace BlazorSPABookStore.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Book>>
                (await _httpClient.GetStreamAsync($"api/books"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Book> GetById(int bookId)
        {
            return await JsonSerializer.DeserializeAsync<Book>
                (await _httpClient.GetStreamAsync($"api/books/{bookId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Book> Add(Book book)
        {
            var bookJson = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/books", bookJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Book>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task<bool> Update(Book book)
        {
            var bookJson = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/books/{book.Id}", bookJson);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> Delete(int bookId)
        {
            var result = await _httpClient.DeleteAsync($"api/books/{bookId}");

            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
            
        }
    }
}
