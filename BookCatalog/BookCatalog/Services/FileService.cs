using BookCatalog.Models;
using System.Text.Json;

namespace BookCatalog.Services
{
    public class FileService
    {
        private readonly string _filePath;

        public FileService(string filePath)
        {
            _filePath = filePath;
        }

        public List<Book> LoadBooks()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    Console.WriteLine("[INFO] A fájl nem létezett, új üres lista jön létre.");
                    return new List<Book>();
                }

                string json = File.ReadAllText(_filePath);

                if (string.IsNullOrWhiteSpace(json))
                    return new List<Book>();

                return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HIBA] Fájlbeolvasási hiba: {ex.Message}");
                return new List<Book>();
            }
        }

        public void SaveBooks(List<Book> booksToSave)
        {
            try
            {

                string updatedJson = JsonSerializer.Serialize(booksToSave, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, updatedJson);

                Console.WriteLine($"Könyvek sikeresen mentve!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HIBA] Mentési hiba: {ex.Message}");
            }
        }
    }
}
