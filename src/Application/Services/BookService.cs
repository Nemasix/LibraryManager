using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Contracts;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IRepositoryManager _repositoryManager;

        public BookService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<BookDto> CreateAsync(BookForCreationDto bookDto, CancellationToken cancellationToken = default)
        {
            var book = bookDto.Adapt<Domain.Entities.Book>();
            _repositoryManager.Book.Add(book);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return book.Adapt<BookDto>();
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            _repositoryManager.Book.Delete(id);
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var books = await _repositoryManager.Book.GetAllAsync(cancellationToken);
            var booksDto = books.Adapt<IEnumerable<BookDto>>();

            return booksDto;
        }

        public async Task<BookDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var book = await _repositoryManager.Book.GetByIdAsync(id, cancellationToken);
            if (book == null)
            {
                throw new BookNotFoundException(id);
            }
            var bookDto = book.Adapt<BookDto>();
            return bookDto;
        }

        public async Task UpdateAsync(Guid id, BookForUpdateDto bookDto, CancellationToken cancellationToken = default)
        {
            var book = await _repositoryManager.Book.GetByIdAsync(id, cancellationToken);
            if(book == null)
            {
                throw new BookNotFoundException(id);
            }

            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.ISBN = bookDto.ISBN;
            book.Publisher = bookDto.Publisher;
            book.PublicationDate = bookDto.PublicationDate;
            book.Language = bookDto.Language;
            book.Genre = bookDto.Genre;
            book.Price = bookDto.Price;
            book.NumberOfPages = bookDto.NumberOfPages;

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}