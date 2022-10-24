using BlazorSPABookStore.Interfaces;
using System.Text.Json;
using System.Text;
using BlazorSPABookStore.Models;
using System.Net.Http.Json;

namespace BlazorSPABookStore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Category>>($"api/categories");

            return response;
        }

        public async Task<Category> GetById(int categoryId)
        {
            var response = await _httpClient.GetFromJsonAsync<Category>($"api/categories/{categoryId}");

            return response;
        }

        public async Task<Category> Add(Category category)
        {
            var categoryJson = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/categories", categoryJson);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent) return default(Category);

                return await JsonSerializer.DeserializeAsync<Category>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task<bool> Update(Category category)
        {
            var categoryJson = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/categories/{category.Id}", categoryJson);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> Delete(int categoryId)
        {
            var result = await _httpClient.DeleteAsync($"api/categories/{categoryId}");

            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
    }
}