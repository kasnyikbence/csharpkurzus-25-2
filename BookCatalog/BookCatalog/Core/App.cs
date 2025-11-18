

using BookCatalog.Services;
using BookCatalog.UI;

namespace BookCatalog.Core
{
    public class App
    {
        public void Start()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "books.json");

            var fileService = new FileService(filePath);
            var bookService = new BookService(fileService);
            var menuManager = new MenuManager(bookService);

            menuManager.Run();
        }
    }
}
