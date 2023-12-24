using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    /// <summary>
    /// Represents a repository for managing books.
    /// </summary>
    public interface IBooksRepository
    {
        /// <summary>
        /// Retrieves all books asynchronously.
        /// </summary>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of books.</returns>
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
        /// Updates an existing book.
        /// </summary>
        /// <param name="book">The book to update.</param>
        void Update(Book book);

        /// <summary>
        /// Deletes a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to delete.</param>
        void Delete(Guid id);
    }
}