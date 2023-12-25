using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Domain.Entities;

namespace Application.Abstractions
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<BookDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BookDto> CreateAsync(BookForCreationDto bookDto, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id, BookForUpdateDto bookDto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}