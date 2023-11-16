using Microsoft.AspNetCore.Mvc;
using OWNA.BookNest.Repository;
using OWNA.BookNest.Repository.Models;

namespace OWNA.BookNest.UI.Controllers
{
    public class AuthorController : Controller
    {
        private readonly BookRepository _bookRepository;
        public AuthorController()
        {
            _bookRepository = new BookRepository();
        }

        public IActionResult Index()
        {
            var allAuthors = _bookRepository.GetAllAuthors();
            return View(allAuthors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.AddAuthor(author);
                return RedirectToAction("Index", "Author");
            }
            return View(author);
        }
    }
}
