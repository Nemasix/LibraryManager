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

        public async Task<BookDto> CreateAsync(BookForCreationDto bookForCreationDto, CancellationToken cancellationToken = default)
        {
            var book = bookForCreationDto.Adapt<Domain.Entities.Book>();
            _repositoryManager.Book.Add(book);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            var owner = await _repositoryManager.User.GetByIdAsync(book.OwnerId, cancellationToken);
            if (owner == null)
            {
                throw new UserNotFoundException(book.OwnerId);
            }
            var bookDto = book.Adapt<BookDto>();
            bookDto.Owner = owner.Adapt<UserDto>();
            return bookDto;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            _repositoryManager.Book.Delete(id);
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var books = await _repositoryManager.Book.GetAllAsync(cancellationToken);
            var booksDto = books.Select(book =>
            {
                var bookDto = book.Adapt<BookDto>();
                bookDto.Owner = _repositoryManager.User.GetByIdAsync(book.OwnerId).Result.Adapt<UserDto>();
                return bookDto;
            });

            return booksDto;
        }

        public async Task<IEnumerable<BookDto>> GetAllByOwnerAsync(Guid ownerId, CancellationToken cancellationToken = default)
        {
            var owner = await _repositoryManager.User.GetByIdAsync(ownerId, cancellationToken);
            if (owner == null)
            {
                throw new UserNotFoundException(ownerId);
            }

            var books = await _repositoryManager.Book.GetAllByOwnerAsync(ownerId);

            var booksDto = books.Select(book =>
            {
                var bookDtoo = book.Adapt<BookDto>();
                bookDtoo.Owner = owner.Adapt<UserDto>();
                return bookDtoo;
            });

            return booksDto;
        }

        public async Task<BookDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var book = await _repositoryManager.Book.GetByIdAsync(id, cancellationToken);
            if (book == null)
            {
                throw new BookNotFoundException(id);
            }
            var owner = await _repositoryManager.User.GetByIdAsync(book.OwnerId, cancellationToken);
            if (owner == null)
            {
                throw new UserNotFoundException(book.OwnerId);
            }
            var bookDto = book.Adapt<BookDto>();
            bookDto.Owner = owner.Adapt<UserDto>();

            return bookDto;
        }

        public async Task UpdateAsync(Guid id, BookForUpdateDto bookForUpdateDto, CancellationToken cancellationToken = default)
        {
            var book = await _repositoryManager.Book.GetByIdAsync(id, cancellationToken);
            if(book == null)
            {
                throw new BookNotFoundException(id);
            }

            book.Title = bookForUpdateDto.Title;
            book.Author = bookForUpdateDto.Author;
            book.ISBN = bookForUpdateDto.ISBN;
            book.OwnerId = bookForUpdateDto.OwnerId ?? book.OwnerId;

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}