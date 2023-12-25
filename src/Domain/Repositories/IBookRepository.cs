using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    /// <summary>
    /// Represents a repository for managing Book.
    /// </summary>
    public interface IBookRepository
    {
        /// <summary>
        /// Retrieves all Book asynchronously.
        /// </summary>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of Book.</returns>
        Task<IEnumerable<Book>> GetAllAsync(CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Retrieves a book by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the book.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the book.</returns>
        Task<Book> GetByIdAsync(Guid id, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Adds a new book.
        /// </summary>
        /// <param name="book">The book to add.</param>
        void Add(Book book);

        /// <summary>
        /// Deletes a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to delete.</param>
        void Delete(Guid id);
    }
}