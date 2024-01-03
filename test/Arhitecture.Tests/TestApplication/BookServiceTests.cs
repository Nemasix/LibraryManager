

using Application.Abstractions;
using Application.Contracts;
using Application.Services;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using Moq;

namespace Tests.TestApplication
{
    public class BookServiceTests
    {

        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly IBookService _bookService;

        public BookServiceTests()
        {
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _bookService = new BookService(_repositoryManagerMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ValidBookForCreationDto_ReturnsBookDto()
        {
            // Arrange
            var bookForCreationDto = new BookForCreationDto
            {
                // Set properties for bookForCreationDto
                Author = "Sample Author",
                ISBN = "1234567890",
                Title = "Sample Title"
            };
            var book = bookForCreationDto.Adapt<Book>();
            _repositoryManagerMock.Setup(r => r.Book.Add(It.IsAny<Book>()));
            _repositoryManagerMock.Setup(r => r.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(default(int)));

            // Act
            var result = await _bookService.CreateAsync(bookForCreationDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BookDto>(result);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_DeletesBook()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repositoryManagerMock.Setup(r => r.Book.Delete(It.IsAny<Guid>()));
            _repositoryManagerMock.Setup(r => r.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(default(int)));

            // Act
            await _bookService.DeleteAsync(id);

            // Assert
            _repositoryManagerMock.Verify(r => r.Book.Delete(id), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsListOfBookDto()
        {
            // Arrange
            var books = new List<Book>
            {
                // Add books to the list
                new Book { Id = Guid.NewGuid(), Title = "Sample Title", Author = "Sample Author", ISBN = "1234567890"},
                new Book { Id = Guid.NewGuid(), Title = "Sample Title 2", Author = "Sample Author 2", ISBN = "2134567890"},
                new Book { Id = Guid.NewGuid(), Title = "Sample Title 3", Author = "Sample Author 3", ISBN = "3134567890"}
            };
            _repositoryManagerMock.Setup(r => r.Book.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(books);

            // Act
            var result = await _bookService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<BookDto>>(result);
        }

        [Fact]
        public async Task GetAllByOwnerAsync_ValidOwnerId_ReturnsListOfBookDto()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            var owner = new User
            {
                // Set properties for owner
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoe",
                Email = "johndoe@example.com",
                Password = "Password123"
            };
            var books = new List<Book>
            {
                // Add books to the list
                new Book { Id = Guid.NewGuid(), Title = "Sample Title", Author = "Sample Author", ISBN = "1234567890"},
                new Book { Id = Guid.NewGuid(), Title = "Sample Title 2", Author = "Sample Author 2", ISBN = "2134567890"},
                new Book { Id = Guid.NewGuid(), Title = "Sample Title 3", Author = "Sample Author 3", ISBN = "3134567890"}
            };
            _repositoryManagerMock.Setup(r => r.User.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(owner);
            _repositoryManagerMock.Setup(r => r.Book.GetAllByOwnerAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(books);

            // Act
            var result = await _bookService.GetAllByOwnerAsync(ownerId);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<BookDto>>(result);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsBookDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var book = new Book
            {
                // Set properties for book
                Author = "Sample Author",
                ISBN = "1234567890",
                Title = "Sample Title"
            };
            _repositoryManagerMock.Setup(r => r.Book.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(book);

            // Act
            var result = await _bookService.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BookDto>(result);
        }

        [Fact]
        public async Task UpdateAsync_ValidId_UpdatesBook()
        {
            // Arrange
            var id = Guid.NewGuid();
            var bookForUpdateDto = new BookForUpdateDto
            {
                // Set properties for bookForUpdateDto
                Author = "Sample Author 2",
                ISBN = "2134567890",
                Title = "Sample Title 2",
                OwnerId = Guid.NewGuid()
            };
            var book = new Book
            {
                // Set properties for book
                Author = "Sample Author",
                ISBN = "1234567890",
                Title = "Sample Title",
                OwnerId = Guid.NewGuid()
            };
            _repositoryManagerMock.Setup(r => r.Book.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(book);
            _repositoryManagerMock.Setup(r => r.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(default(int)));

            // Act
            await _bookService.UpdateAsync(id, bookForUpdateDto);

            // Assert
            Assert.Equal(bookForUpdateDto.Title, book.Title);
            Assert.Equal(bookForUpdateDto.Author, book.Author);
            Assert.Equal(bookForUpdateDto.ISBN, book.ISBN);
            Assert.Equal(bookForUpdateDto.OwnerId ?? book.OwnerId, book.OwnerId);
        }
    }
}
