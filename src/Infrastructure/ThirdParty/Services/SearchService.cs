using Application.Abstractions;
using Application.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace Infrastructure.ThirdParty.Services
{
    public class SearchService : ISearchService
    {
        private readonly IConfiguration Configuration;
        private readonly string? ApiUrl;
        private readonly HttpClient httpClient;

        public SearchService(IConfiguration configuration)
        {
            Configuration = configuration;
            ApiUrl = Configuration.GetSection("ApiUrl").Value;
            if (ApiUrl == null)
            {
                throw new Exception("ApiUrl is not configured");
            }
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ApiUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ResultDto> Search(SearchDto searchDto)
        {
            var response = await httpClient.GetAsync($"search.json?q={GetQuery(searchDto)}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = await response.Content.ReadFromJsonAsync<ResultDto>();
                return result ?? new ResultDto();
            }
            else
            {
                throw new Exception($"Error calling the API: {response.ReasonPhrase}");
            }
        }

        private string GetQuery(SearchDto searchDto)
        {
            var query = new StringBuilder();
            if (!string.IsNullOrEmpty(searchDto.Title))
            {
                query.Append($"title:{searchDto.Title}");
            }
            if (!string.IsNullOrEmpty(searchDto.Author))
            {
                if (query.Length > 0)
                {
                    query.Append(" OR ");
                }
                query.Append($"author:{searchDto.Author}");
            }
            if(searchDto.Page.HasValue)
            {
                query.Append($"&page={searchDto.Page}");
            }
            if(searchDto.Limit.HasValue)
            {
                query.Append($"&limit={searchDto.Limit}");
            }
            return query.ToString();
        }
    }
}
