using Blazored.Toast;
using BlazorSPABookStore;
using BlazorSPABookStore.Interfaces;
using BlazorSPABookStore.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Example 1:
builder.Services.AddHttpClient<ICategoryService, CategoryService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BookStoreApi:Url"]);
});

// Example 2:
builder.Services.AddHttpClient();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddBlazoredToast();

await builder.Build().RunAsync();