using BlazorSPABookStore.Models;

namespace BlazorSPABookStore.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetById(int bookId);
        Task<Book> Add(Book book);
        Task<bool> Update(Book book);
        Task<bool> Delete(int bookId);
    }
}