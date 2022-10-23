using BlazorSPABookStore.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text;
using BlazorSPABookStore.Models;
using System.Data;
using System.Runtime.InteropServices;

namespace BlazorSPABookStore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var result = await JsonSerializer.DeserializeAsync<IEnumerable<Category>>
                (await _httpClient.GetStreamAsync($"api/categories"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result;
        }

        public async Task<Category> GetById(int categoryId)
        {
            return await JsonSerializer.DeserializeAsync<Category>
                (await _httpClient.GetStreamAsync($"api/categories/{categoryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Category> Add(Category category)
        {
            var categoryJson = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/categories", categoryJson);

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
