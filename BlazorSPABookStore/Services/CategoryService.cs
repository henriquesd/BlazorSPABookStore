using BlazorSPABookStore.Interfaces;
using System.Text.Json;
using System.Text;
using BlazorSPABookStore.Models;
using System.Net.Http.Json;

namespace BlazorSPABookStore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUri;

        public CategoryService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _baseUri = _configuration.GetSection("BookStoreApi:Url").Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetFromJsonAsync<IEnumerable<Category>>($"{_baseUri}api/categories");

            return response;
        }

        public async Task<Category> GetById(int categoryId)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetFromJsonAsync<Category>($"{_baseUri}api/categories/{categoryId}");

            return response;
        }

        public async Task<Category> Add(Category category)
        {
            var httpClient = _httpClientFactory.CreateClient(_baseUri);

            var categoryJson = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{_baseUri}api/categories", categoryJson);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent) return default(Category);

                return await JsonSerializer.DeserializeAsync<Category>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task<bool> Update(Category category)
        {
            var httpClient = _httpClientFactory.CreateClient(_baseUri);

            var categoryJson = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync($"{_baseUri}api/categories/{category.Id}", categoryJson);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> Delete(int categoryId)
        {
            var httpClient = _httpClientFactory.CreateClient(_baseUri);

            var result = await httpClient.DeleteAsync($"{_baseUri}api/categories/{categoryId}");

            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
    }
}