using Microsoft.AspNetCore.Mvc;
using OWNA.BookNest.Repository;
using OWNA.BookNest.Repository.Models;

namespace OWNA.BookNest.UI.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository;

        public BookController()
        {
            _bookRepository = new BookRepository();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var allBooks = _bookRepository.GetAllBooks();
            return View(allBooks);
        }

        [HttpGet]
        public IActionResult Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                var allBooks = _bookRepository.GetAllBooks();
                return View("Index", allBooks);
            }

            var searchResult = _bookRepository.SearchBooks(searchText);
            return View("Index", searchResult);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var book = new Book
            {
                Authors = GetAvailableAuthors() 
            };
            return View(book);
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {

            if (ModelState.IsValid)
            {
                book.Authors = book.SelectedAuthorIds.Select(authorId => _bookRepository.GetAuthorById(authorId)).ToList();

                _bookRepository.AddBook(book);
                return RedirectToAction("Index");
            }
            book.Authors = GetAvailableAuthors(); 
            return View(book);
        }

        private List<Author> GetAvailableAuthors()
        {
            var allAuthors = _bookRepository.GetAllAuthors().ToList(); 

            return allAuthors;
        }
    }
}
