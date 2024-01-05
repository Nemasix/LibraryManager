using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppLibraryManager.Contracts;
using WebAppLibraryManager.Services;

namespace WebAppLibraryManager.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<LoansController> _logger;
        private readonly IServiceManager _serviceManager;
        private IConfiguration Configuration;
        public SearchController(ILogger<LoansController> logger, IServiceManager serviceManager, IConfiguration configuration)
        {
            _logger = logger;
            _serviceManager = serviceManager;
            Configuration = configuration;
        }

        // GET: SearchController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Search([FromBody] SearchDto searchDto)
        {
            try
            {
                var result = await _serviceManager.SearchService.Search(searchDto);
                if (result == null)
                {
                    return Json(new { });
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching");
                return Json(new { });
            }
        }
    }
}
