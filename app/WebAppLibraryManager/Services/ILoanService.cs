using WebAppLibraryManager.Contracts;

namespace WebAppLibraryManager;

public interface ILoanService
{
    Task<LoanDto> GetLoanAsync(Guid id);
    Task<IEnumerable<LoanDto>> GetLoansAsync();
    Task<LoanDto> CreateLoanAsync(LoanForCreationDto loan);
    Task UpdateLoanAsync(Guid id, LoanForUpdateDto loan);
    Task DeleteLoanAsync(Guid id);
    Task<IEnumerable<LoanDto>> GetLoansByBookIdAsync(Guid bookId);
    Task<IEnumerable<LoanDto>> GetLoansByLoanerIdAsync(Guid loanerId);
}
