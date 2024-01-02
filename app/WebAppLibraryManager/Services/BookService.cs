using WebAppLibraryManager.Contracts;

namespace WebAppLibraryManager.Services
{
    public class BookService : IBookService
    {
        private readonly IConfiguration Configuration;
        private readonly ApiClient ApiClient;

        public BookService(IConfiguration configuration)
        {
            Configuration = configuration;
            ApiClient = new ApiClient(Configuration);
        }

        public async Task<BookDto> GetBookAsync(Guid id)
        {
            return await ApiClient.GetAsync<BookDto>($"books/{id}");
        }

        public async Task<IEnumerable<BookDto>> GetBooksAsync()
        {
            return await ApiClient.GetAsync<IEnumerable<BookDto>>("books");
        }

        public async Task<BookDto> CreateBookAsync(BookForCreationDto book)
        {
            return await ApiClient.PostAsync<BookForCreationDto, BookDto>("books", book);
        }

        public async Task UpdateBookAsync(Guid id, BookForUpdateDto book)
        {
            await ApiClient.PutAsync<BookForUpdateDto>($"books/{id}", book);
        }

        public async Task DeleteBookAsync(Guid id)
        {
            await ApiClient.DeleteAsync($"books/{id}");
        }
    }
}
