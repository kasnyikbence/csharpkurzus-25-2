
using BookCatalog.Models;
using BookCatalog.Services;

namespace BookCatalog.UI
{
    public class MenuManager
    {
        private readonly BookService _bookService;

        public MenuManager(BookService bookService)
        {
            _bookService = bookService;
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n---- Könyvkatalógus Menü ----");
                Console.WriteLine("1. Összes könyv listázása");
                Console.WriteLine("2. Új könyv hozzáadása");
                Console.WriteLine("3. Szerzők könyveinek száma");
                Console.WriteLine("4. Legolcsóbb könyv");
               // Console.WriteLine("5. Mentés  fájlba");
                Console.WriteLine("0. Kilépés");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1": ListAllBooks(); break;
                    case "2": AddNewBook(); break;
                    case "3": _bookService.DisplayAuthorsByBookCount(); break;
                    case "4": DisplayCheapestBook(); break;
                    case "5": _bookService.Save(); break;
                    case "0": running = false; break;
                    default: Console.WriteLine("Érvénytelen választás"); break;

                }
            }
        }

        private void DisplayCheapestBook()
        {
            var book = _bookService.GetCheapestBook();
            if (book != null)
            {
                Console.WriteLine($"\nA legolcsóbb könyv: {book.Title} ({book.Author}, {book.Year}, {book.Price})");
            }
            else
            {
                Console.WriteLine("Üres a könyvkatalógus");
            }
        }

        private void AddNewBook()
        {
            try
            {
                Console.Write("Cím: ");
                string title = Console.ReadLine() ?? "";
                Console.Write("Szerző: ");
                string author = Console.ReadLine() ?? "";
                Console.Write("Kiadási év: ");
                int year = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Ár: ");
                double price = double.Parse(Console.ReadLine() ?? "0");

                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) || year <= 0 || price < 0)
                {
                    Console.WriteLine("[HIBA] Érvénytelen adatok");
                    return;
                }

                _bookService.AddBook(new Book(title, author, year, price));
            }
            catch (FormatException) 
            {
                Console.WriteLine("[HIBA] Adatbeviteli hiba");
            }
        }

        private void ListAllBooks()
        {
            var books = _bookService.GetAllBooks();
            if (!books.Any())
            {
                Console.WriteLine("Nincsenek könyvek a katalógusban");
                return;
            }

            Console.WriteLine("\nÖsszes könyv:");

            foreach (var book in books)
            {
                Console.WriteLine($"- {book.Title} ({book.Author}, {book.Year}, {book.Price})");
            }
        }
    }
}
