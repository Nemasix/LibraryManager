using Application.Abstractions;
using Application.Contracts;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    internal class LoanService : ILoanService
    {
        private readonly IRepositoryManager _repositoryManager;

        public LoanService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<LoanDto> CreateAsync(LoanForCreationDto loanForCreationDto, CancellationToken cancellationToken = default)
        {
            var loan = loanForCreationDto.Adapt<Domain.Entities.Loan>();
            _repositoryManager.Loan.Add(loan);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return loan.Adapt<LoanDto>();
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            _repositoryManager.Loan.Delete(id);
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<LoanDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var loans = await _repositoryManager.Loan.GetAllAsync(cancellationToken);
            return loans.Adapt<IEnumerable<LoanDto>>();
        }

        public async Task<IEnumerable<LoanDto>> GetAllByBookAsync(Guid bookId, CancellationToken cancellationToken = default)
        {
            var loans = await _repositoryManager.Loan.GetAllByBookAsync(bookId);
            return loans.Adapt<IEnumerable<LoanDto>>();

        }

        public async Task<IEnumerable<LoanDto>> GetAllByLoanerAsync(Guid loanerId, CancellationToken cancellationToken = default)
        {
            var loans = await _repositoryManager.Loan.GetAllByLoanerAsync(loanerId);
            return loans.Adapt<IEnumerable<LoanDto>>();
        }

        public async Task<LoanDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var loan = await _repositoryManager.Loan.GetByIdAsync(id, cancellationToken);
            return loan.Adapt<LoanDto>();
        }

        public async Task UpdateAsync(Guid id, LoanForUpdateDto loanForUpdateDto, CancellationToken cancellationToken = default)
        {
            var loan = await _repositoryManager.Loan.GetByIdAsync(id, cancellationToken);
            if (loan == null)
            {
                throw new LoanNotFoundException(id);
            }
            loan.LoanDate = loanForUpdateDto.LoanDate;
            loan.ReturnDate = loanForUpdateDto.ReturnDate;   
            loan.BookId = loanForUpdateDto.BookId;
            loan.LoanerId = loanForUpdateDto.LoanerId;

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
