using Application.Abstractions;
using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public BooksController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks(CancellationToken ct)
        {
            var books = await _serviceManager.BookService.GetAllAsync(ct);
            return Ok(books);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBookById(Guid id, CancellationToken ct)
        {
            var book = await _serviceManager.BookService.GetByIdAsync(id, ct);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody]BookForCreationDto bookForCreationDto)
        {
            var bookDto = await _serviceManager.BookService.CreateAsync(bookForCreationDto);
            return CreatedAtAction(nameof(GetBookById), new { id = bookDto.Id }, bookDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody]BookForUpdateDto bookForUpdateDto, CancellationToken ct)
        {
            await _serviceManager.BookService.UpdateAsync(id, bookForUpdateDto, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBook(Guid id, CancellationToken ct)
        {
            await _serviceManager.BookService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}