using WebAppLibraryManager.Contracts;

namespace WebAppLibraryManager.Services
{
    public interface ISearchService
    {
        Task<ResultDto> Search(SearchDto search);
    }
}
