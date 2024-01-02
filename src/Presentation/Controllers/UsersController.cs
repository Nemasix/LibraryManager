using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public UsersController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(CancellationToken ct)
        {
            var users = await _serviceManager.UserService.GetAllAsync(ct);
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken ct)
        {
            var user = await _serviceManager.UserService.GetByIdAsync(id, ct);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]UserForCreationDto userForCreationDto)
        {
            var userDto = await _serviceManager.UserService.CreateAsync(userForCreationDto);
            return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody]UserForUpdateDto userForUpdateDto, CancellationToken ct)
        {
            await _serviceManager.UserService.UpdateAsync(id, userForUpdateDto, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken ct)
        {
            await _serviceManager.UserService.DeleteAsync(id, ct);
            return NoContent();
        }

        [HttpGet("{id:guid}/loans")]
        public async Task<IActionResult> GetLoansByUserId(Guid id, CancellationToken ct)
        {
            var loans = await _serviceManager.LoanService.GetAllByLoanerAsync(id, ct);
            return Ok(loans);
        }

        [HttpGet("{id:guid}/books")]
        public async Task<IActionResult> GetBooksByUserId(Guid id, CancellationToken ct)
        {
            var books = await _serviceManager.BookService.GetAllByOwnerAsync(id, ct);
            return Ok(books);
        }
    }
}