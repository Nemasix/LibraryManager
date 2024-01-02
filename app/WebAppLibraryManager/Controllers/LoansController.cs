using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using WebAppLibraryManager.Contracts;
using WebAppLibraryManager.Services;

namespace WebAppLibraryManager;

public class LoansController : Controller
{
    private readonly ILogger<LoansController> _logger;
    private readonly IServiceManager _serviceManager;
    private IConfiguration Configuration;
    public LoansController(ILogger<LoansController> logger, IServiceManager serviceManager, IConfiguration configuration)
    {
        _logger = logger;
        _serviceManager = serviceManager;
        Configuration = configuration;
    }

    // GET: LoansController
    public async Task<ActionResult> Index()
    {
        var loans = await _serviceManager.LoanService.GetLoansAsync();

        var loanTasks = loans.Select(async loans =>
        {
            loans.Book = await _serviceManager.BookService.GetBookAsync(loans.BookId);
            loans.Loaner = await _serviceManager.UserService.GetUserAsync(loans.LoanerId);
            return loans;
        });

        loans = await Task.WhenAll(loanTasks);

        return View(loans);
    }

    // GET: LoansController/Details/5
    public async Task<ActionResult> Details(Guid id)
    {
        var loan = await _serviceManager.LoanService.GetLoanAsync(id);
        loan.Book = await _serviceManager.BookService.GetBookAsync(loan.BookId);
        loan.Loaner = await _serviceManager.UserService.GetUserAsync(loan.LoanerId);
        return View(loan);
    }

    // GET: LoansController/Create
    public async Task<ActionResult> Create()
    {
        ViewData["ApiUrl"] = Configuration.GetSection("ApiSettings").GetValue<string>("Url");
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
        ViewData["ApiUrl"] = Configuration.GetSection("ApiSettings").GetValue<string>("Url");
        var loan = await _serviceManager.LoanService.GetLoanAsync(id);
        LoanForUpdateDto loanForUpdateDto = loan.Adapt<LoanForUpdateDto>();
        return View(loanForUpdateDto);
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
        loan.Book = await _serviceManager.BookService.GetBookAsync(loan.BookId);
        loan.Loaner = await _serviceManager.UserService.GetUserAsync(loan.LoanerId);
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
