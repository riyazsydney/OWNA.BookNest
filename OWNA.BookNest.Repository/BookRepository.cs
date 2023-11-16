using Newtonsoft.Json;
using OWNA.BookNest.Repository.Models;

namespace OWNA.BookNest.Repository
{
    public class BookRepository
    {
        private readonly List<Author> _authors;
        private readonly List<Book> _books;

        public BookRepository() 
        {
            var jsonData = File.ReadAllText("Data/books.json");
            var data = JsonConvert.DeserializeObject<JsonData>(jsonData);

            if (data != null ) 
            {
                _authors = data.Authors;
                _books = data.Books;
            }
            
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _authors;
        }

        public void AddAuthor(Author author)
        {
            author.Id = _authors.Count > 0 ? _authors.Max(a => a.Id) + 1 : 1;
            _authors.Add(author);
            SaveChangesToJson();
        }

        public Author GetAuthorById(int authorId) 
        {
            return _authors.FirstOrDefault(a => a.Id == authorId);
        }


        public IEnumerable<Book> SearchBooks(string searchText)
        {
            return _books.Where(b =>
                b.Title.Contains(searchText, System.StringComparison.OrdinalIgnoreCase) ||
                b.Authors.Any(a => a.Name.Contains(searchText, System.StringComparison.OrdinalIgnoreCase))
            );
        }

        public void AddBook(Book book)
        {
            book.Id = _books.Count > 0 ? _books.Max(b => b.Id) + 1 : 1;
            _books.Add(book);
            SaveChangesToJson();
        }

        private void SaveChangesToJson()
        {
            var jsonData = JsonConvert.SerializeObject(new JsonData { Authors = _authors, Books = _books }, Formatting.Indented);
            File.WriteAllText("Data/books.json", jsonData);
        }
    }

    public class JsonData
    {
        public List<Author> Authors { get; set; }
        public List<Book> Books { get; set; }
    }
}
