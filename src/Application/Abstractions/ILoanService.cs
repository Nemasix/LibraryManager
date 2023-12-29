using Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<LoanDto>> GetAllByBookAsync(Guid bookId, CancellationToken cancellationToken = default);
        Task<IEnumerable<LoanDto>> GetAllByLoanerAsync(Guid loanerId, CancellationToken cancellationToken = default);
        Task<LoanDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<LoanDto> CreateAsync(LoanForCreationDto loanDto, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id, LoanForUpdateDto loanDto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
