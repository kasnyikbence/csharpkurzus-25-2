using BookCatalog.Models;
using BookCatalog.Services;

namespace BookCatalog
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "books.json");
           // Console.WriteLine($"JSON fájl helye: {filePath}");
            var fileService = new FileService(filePath);

            var newBooks = new List<Book>
            {
                new Book("A Gyűrűk Ura", "J. R. R. Tolkien", 1954, 4990),
                new Book("Állatfarm", "George Orwell", 1945, 2590)
            };

            fileService.SaveBooks(newBooks);

            var allBooks = fileService.LoadBooks();

            Console.WriteLine("Jelenlegi könyvek:");
            foreach (var book in allBooks)
            {
                Console.WriteLine($"- {book.Title} ({book.Author}, {book.Year}, {book.Price})");
            }
        }
    }
}
