using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppLibraryManager.Contracts;
using WebAppLibraryManager.Models;
using WebAppLibraryManager.Services;

namespace WebAppLibraryManager.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IServiceManager _serviceManager;
        public UsersController(ILogger<UsersController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }
        // GET: UserController
        public async Task<ActionResult> Index()
        {
            var users = await _serviceManager.UserService.GetUsersAsync();

            return View(users);
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var user = await _serviceManager.UserService.GetUserAsync(id);
            return View(user);
        }

        // GET: UserController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserForCreationDto user)
        {
            try
            {
                var result = await _serviceManager.UserService.CreateUserAsync(user);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var user = await _serviceManager.UserService.GetUserAsync(id);
            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, UserDto user)
        {
            try
            {
                var result = await _serviceManager.UserService.UpdateUserAsync(id, user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, UserDto user)
        {
            try
            {
                await _serviceManager.UserService.DeleteUserAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
