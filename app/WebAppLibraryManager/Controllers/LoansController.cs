using Microsoft.AspNetCore.Mvc;
using WebAppLibraryManager.Contracts;
using WebAppLibraryManager.Services;

namespace WebAppLibraryManager;

public class LoansController : Controller
{
     private readonly ILogger<LoansController> _logger;
        private readonly IServiceManager _serviceManager;
        public LoansController(ILogger<LoansController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        // GET: LoansController
        public async Task<ActionResult> Index()
        {
            var loans = await _serviceManager.LoanService.GetLoansAsync();

            return View(loans);
        }

        // GET: LoansController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var loan = await _serviceManager.LoanService.GetLoanAsync(id);
            return View(loan);
        }

        // GET: LoansController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: LoansController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LoanForCreationDto loan)
        {
            try
            {
                var result = await _serviceManager.LoanService.CreateLoanAsync(loan);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoansController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var loan = await _serviceManager.LoanService.GetLoanAsync(id);
            return View(loan);
        }

        // POST: LoansController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, LoanForUpdateDto loan)
        {
            try
            {
                await _serviceManager.LoanService.UpdateLoanAsync(id, loan);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoansController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var loan = await _serviceManager.LoanService.GetLoanAsync(id);
            return View(loan);
        }

        // POST: LoansController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, LoanDto user)
        {
            try
            {
                await _serviceManager.LoanService.DeleteLoanAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
}
