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
    public class LoanController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public LoanController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetLoans(CancellationToken ct)
        {
            var loans = await _serviceManager.LoanService.GetAllAsync(ct);
            return Ok(loans);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetLoanById(Guid id, CancellationToken ct)
        {
            var loan = await _serviceManager.LoanService.GetByIdAsync(id, ct);
            if (loan == null)
            {
                return NotFound();
            }
            return Ok(loan);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoan([FromBody]LoanForCreationDto loanForCreationDto)
        {
            var loanDto = await _serviceManager.LoanService.CreateAsync(loanForCreationDto);
            return CreatedAtAction(nameof(GetLoanById), new { id = loanDto.Id }, loanDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateLoan(Guid id, [FromBody]LoanForUpdateDto loanForUpdateDto, CancellationToken ct)
        {
            await _serviceManager.LoanService.UpdateAsync(id, loanForUpdateDto, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteLoan(Guid id, CancellationToken ct)
        {
            await _serviceManager.LoanService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}