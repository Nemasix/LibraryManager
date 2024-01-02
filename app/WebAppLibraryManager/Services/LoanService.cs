using WebAppLibraryManager.Contracts;
using WebAppLibraryManager.Services;

namespace WebAppLibraryManager;

public class LoanService : ILoanService
{
        private readonly IConfiguration Configuration;
        private readonly ApiClient ApiClient;
        public LoanService(IConfiguration configuration)
        {
            Configuration = configuration;
            ApiClient = new ApiClient(Configuration);
        }

        public async Task<LoanDto> GetLoanAsync(Guid id)
        {
            return await ApiClient.GetAsync<LoanDto>($"loans/{id}");
        }

        public async Task<IEnumerable<LoanDto>> GetLoansAsync()
        {
            return await ApiClient.GetAsync<IEnumerable<LoanDto>>("loans");
        }

        public async Task<LoanDto> CreateLoanAsync(LoanForCreationDto loan)
        {
            return await ApiClient.PostAsync<LoanForCreationDto, LoanDto>("loans", loan);
        }

        public async Task UpdateLoanAsync(Guid id, LoanForUpdateDto loan)
        {
            await ApiClient.PutAsync<LoanForUpdateDto>($"loans/{id}", loan);
        }

        public async Task DeleteLoanAsync(Guid id)
        {
            await ApiClient.DeleteAsync($"loans/{id}");
        }

        public async Task<IEnumerable<LoanDto>> GetLoansByBookIdAsync(Guid bookId)
        {
            return await ApiClient.GetAsync<IEnumerable<LoanDto>>($"loans/book/{bookId}");
        }

        public async Task<IEnumerable<LoanDto>> GetLoansByLoanerIdAsync(Guid loanerId)
        {
            return await ApiClient.GetAsync<IEnumerable<LoanDto>>($"loans/loaner/{loanerId}");
        }
}
