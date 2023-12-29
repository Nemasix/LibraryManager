using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    /// <summary>
    /// Represents a repository for managing Loan.
    /// </summary>
    public interface ILoanRepository
    {
        /// <summary>
        /// Retrieves all Loan asynchronously.
        /// </summary>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of Loan.</returns>
        Task<IEnumerable<Loan>> GetAllAsync(CancellationToken ct = default);

        /// <summary>
        /// Retrieves all Loan by loaner asynchronously.
        /// </summary>
        /// <param name="loanerId">The loaner Id.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of Loan.</returns>
        Task<IEnumerable<Loan>> GetAllByLoanerAsync(Guid loanerId, CancellationToken ct = default);

        /// <summary>
        /// Retrieves all Loan by book asynchronously.
        /// </summary>
        /// <param name="bookId">The book Id.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of Loan.</returns>
        Task<IEnumerable<Loan>> GetAllByBookAsync(Guid bookId, CancellationToken ct = default);

        /// <summary>
        /// Retrieves a loan by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the loan.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the loan.</returns>
        Task<Loan> GetByIdAsync(Guid id, CancellationToken ct = default);

        /// <summary>
        /// Adds a new loan.
        /// </summary>
        /// <param name="loan">The loan to add.</param>
        void Add(Loan loan);

        /// <summary>
        /// Deletes a loan by its ID.
        /// </summary>
        /// <param name="id">The ID of the loan to delete.</param>
        void Delete(Guid id);
    }
}
