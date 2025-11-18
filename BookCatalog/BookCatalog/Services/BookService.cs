using BookCatalog.Models;

namespace BookCatalog.Services
{
    public class BookService
    {
        private readonly List<Book> _books;
        private readonly FileService _fileService;

        public BookService(FileService fileService)
        {
            _fileService = fileService;
            _books = _fileService.LoadBooks();
            Console.WriteLine($"[INFO] {_books.Count} könyv betöltve.");
        }

        public List<Book> GetAllBooks()
        {
            return _books;
        }

        public void AddBook(Book newBook)
        {
            bool exist = _books
                .Any(b => b.Title.Equals(newBook.Title, StringComparison.OrdinalIgnoreCase) &&
                b.Author.Equals(newBook.Author, StringComparison.OrdinalIgnoreCase));

            if (exist)
            {
                Console.WriteLine($"[INFO] ez a könyv már szerepel a katalógusban: {newBook.Title}");
            }
            else
            {
                _books.Add(newBook);
                _fileService.SaveBooks(_books);
                Console.WriteLine($"Könyv sikeresen hozzáadva: {newBook.Title}");

            }
        }

        public void Save()
        {
            _fileService.SaveBooks(_books);
            Console.WriteLine("Sikeresen mentve");        
        }



        //LINQ szűrések

        public void DisplayAuthorsByBookCount()
        {
            Console.WriteLine("\nSzerzők könyveik száma szerint:");
            var authorsByCount = _books
                .GroupBy(b => b.Author)
                .Select(g => new { Author = g.Key, Count = g.Count() })
                .OrderByDescending(a => a.Count);

            foreach (var item in authorsByCount)
            {
                Console.WriteLine($"- {item.Author}: {item.Count}");
            }
        }

        
        public Book? GetCheapestBook()
        {
            if (!_books.Any()) return null;

            return _books.OrderBy(b => b.Price).FirstOrDefault();

        }






    }
}
