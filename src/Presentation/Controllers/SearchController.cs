using Application.Abstractions;
using Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        
        public async Task<IActionResult> Search([FromQuery]SearchDto search)
        {
            var result = await _searchService.Search(search);
            return Ok(result);
        }
    }
}
