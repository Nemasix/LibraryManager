using WebAppLibraryManager.Contracts;

namespace WebAppLibraryManager.Services
{
    public interface IBookService
    {
        Task<BookDto> GetBookAsync(Guid id);
        Task<IEnumerable<BookDto>> GetBooksAsync();
        Task<BookDto> CreateBookAsync(BookForCreationDto book);
        Task UpdateBookAsync(Guid id, BookForUpdateDto book);
        Task DeleteBookAsync(Guid id);
    }
}
