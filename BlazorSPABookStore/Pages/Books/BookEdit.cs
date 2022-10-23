using Blazored.Toast.Services;
using BlazorSPABookStore.Interfaces;
using BlazorSPABookStore.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorSPABookStore.Pages.Books
{
    public partial class BookEdit
    {
        [Parameter]
        public int BookId { get; set; }

        [Inject]
        public IBookService BookService { get; set; }

        [Inject]
        public ICategoryService CategoryService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        public Book Book { get; set; } = new Book();

        public IEnumerable<Category> CategoryList { get; set; } = new List<Category>();

        protected string Title = "Add new Book";

        protected override async Task OnInitializedAsync()
        {
            CategoryList = await CategoryService.GetAll();

            if (BookId != 0)
            {
                Book = await BookService.GetById(BookId);
                Title = $"Edit {Book.Name}";
            }
        }

        protected async Task HandleValidSubmit()
        {
            if (Book.Id == 0)
            {
                var bookAdded = await BookService.Add(Book);
                if (bookAdded != null)
                {
                    ToastService.ShowSuccess("New book successfully added.");
                    NavigateToBooksPage();
                }
                else
                {
                    ToastService.ShowError("Something went wrong while adding the new book. Please try again.");
                }
            }
            else
            {
                var result = await BookService.Update(Book);
                if (result)
                {
                    ToastService.ShowSuccess("The book was successfully updated.");
                    NavigateToBooksPage();
                }
                else
                {
                    ToastService.ShowError("Something went wrong while updating the book. Please try again.");
                }
            }
        }

        protected void NavigateToBooksPage()
        {
            NavigationManager.NavigateTo("/books");
        }
    }
}
