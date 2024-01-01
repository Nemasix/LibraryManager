using System.Net.Http.Headers;

namespace WebAppLibraryManager.Services
{
    public class ApiClient
    {
        private readonly HttpClient httpClient;
        private IConfiguration Configuration;

        public ApiClient(IConfiguration configuration)
        {
            Configuration = configuration;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(configuration.GetSection("ApiSettings").GetValue<string>("Url"));
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetAsync<T>(string path)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/{path}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<T>();
                }
                else
                {
                    throw new Exception($"Error calling the API: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calling the API: {ex.Message}");
            }
        }

        public async Task DeleteAsync(string path)
        {
            var response = await httpClient.DeleteAsync($"api/{path}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error calling the API: {response.ReasonPhrase}");
            }
        }

        public async Task<TResult> PostAsync<T1, TResult>(string path, T1 item)
        {
            var response = await httpClient.PostAsJsonAsync($"api/{path}", item);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TResult>();
            }
            else
            {
                throw new Exception($"Error calling the API: {response.ReasonPhrase}");
            }
        }

        public async Task PutAsync<T>(string path, T item)
        {
            var response = await httpClient.PutAsJsonAsync($"api/{path}", item);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error calling the API: {response.ReasonPhrase}");
            }
        }

        public async Task<TResult> PatchAsync<T1, TResult>(string path, T1 item)
        {
            var response = await httpClient.PatchAsJsonAsync($"api/{path}", item);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TResult>();
            }
            else
            {
                throw new Exception($"Error calling the API: {response.ReasonPhrase}");
            }
        }
    }
}
