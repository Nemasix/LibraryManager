using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppLibraryManager.Contracts;
using WebAppLibraryManager.Services;

namespace WebAppLibraryManager.Controllers
{
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IServiceManager _serviceManager;
        private IConfiguration Configuration;

        public BooksController(ILogger<BooksController> logger, IServiceManager serviceManager, IConfiguration configuration)
        {
            _logger = logger;
            _serviceManager = serviceManager;
            Configuration = configuration;
        }

        // GET: BooksController
        public async Task<ActionResult> Index()
        {
            var books = await _serviceManager.BookService.GetBooksAsync();

            var ownerTasks = books.Select(async books =>
            {
                books.Owner = await _serviceManager.UserService.GetUserAsync(books.OwnerId);
                return books;
            });

            books = await Task.WhenAll(ownerTasks);

            return View(books);
        }

        // GET: BooksController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var book = await _serviceManager.BookService.GetBookAsync(id);
            book.Owner = await _serviceManager.UserService.GetUserAsync(book.OwnerId);
            return View(book);
        }

        // GET: BooksController/Create
        public async Task<ActionResult> Create()
        {
            ViewData["ApiUrl"] = Configuration.GetSection("ApiSettings").GetValue<string>("Url");
            return View();
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookForCreationDto book)
        {
            try
            {
                var result = await _serviceManager.BookService.CreateBookAsync(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BooksController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var book = await _serviceManager.BookService.GetBookAsync(id);
            BookForUpdateDto bookForUpdateDto = book.Adapt<BookForUpdateDto>();

            ViewData["ApiUrl"] = Configuration.GetSection("ApiSettings").GetValue<string>("Url");

            return View(bookForUpdateDto);
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, BookForUpdateDto book)
        {
            try
            {
                await _serviceManager.BookService.UpdateBookAsync(id, book);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BooksController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var book = await _serviceManager.BookService.GetBookAsync(id);
            book.Owner = await _serviceManager.UserService.GetUserAsync(book.OwnerId);
            return View(book);
        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, LoanDto user)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
