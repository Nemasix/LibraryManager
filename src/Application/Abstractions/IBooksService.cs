using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Domain.Entities;

namespace Application.Abstractions
{
    public interface IBooksService
    {
        Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<BookDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BookDto> CreateAsync(BookDto bookDto, CancellationToken cancellationToken = default);
        Task<BookDto> UpdateAsync(Guid id, BookDto bookDto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}