using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebAppLibraryManager.Contracts;

namespace WebAppLibraryManager.Services
{
    public class SearchService : ISearchService
    {
        private readonly IConfiguration Configuration;
        private readonly ApiClient ApiClient;
        public SearchService(IConfiguration configuration) 
        {
            Configuration = configuration;
            ApiClient = new ApiClient(Configuration);
        }

        public async Task<ResultDto> Search(SearchDto search)
        {
            var result = await ApiClient.GetAsync<ResultDto>($"search{GetQuery(search)}");

            return result.Adapt<ResultDto>();
        }

        private string GetQuery(SearchDto search)
        {
            var query = new StringBuilder();
            query.Append("?");
            if (!string.IsNullOrEmpty(search.Title))
            {
                query.Append($"title={search.Title}");
            }
            if (!string.IsNullOrEmpty(search.Author))
            {
                if (query.Length > 0)
                {
                    query.Append("&");
                }
                query.Append($"author={search.Author}");
            }
            return query.ToString();
        }
    }
}
