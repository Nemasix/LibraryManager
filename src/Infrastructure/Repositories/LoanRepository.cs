using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal class LoanRepository : ILoanRepository
    {
        private readonly LibraryManagerDbContext _dbContext;
        public LoanRepository(LibraryManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Loan loan)
        {
            _dbContext.Loans.Add(loan);
        }

        public void Delete(Guid id)
        {
            _dbContext.Loans.Remove(new Loan { Id = id });
        }

        public async Task<IEnumerable<Loan>> GetAllAsync(CancellationToken ct = default)
        {
            return await _dbContext.Loans.ToListAsync(ct);
        }

        public async Task<IEnumerable<Loan>> GetAllByBookAsync(Guid bookId, CancellationToken ct = default)
        {
            return await _dbContext.Loans.Where(l => l.BookId == bookId).ToListAsync(ct);
        }

        public async Task<IEnumerable<Loan>> GetAllByLoanerAsync(Guid loanerId, CancellationToken ct = default)
        {
            return await _dbContext.Loans.Where(l => l.LoanerId == loanerId).ToListAsync(ct);
        }

        public async Task<Loan> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbContext.Loans.FirstOrDefaultAsync(l => l.Id == id, ct);
        }
    }
}
