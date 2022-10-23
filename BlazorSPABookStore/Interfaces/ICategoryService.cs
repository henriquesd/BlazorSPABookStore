using BlazorSPABookStore.Models;

namespace BlazorSPABookStore.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int categoryId);
        Task<Category> Add(Category category);
        Task<bool> Update(Category category);
        Task<bool> Delete(int categoryId);
    }
}