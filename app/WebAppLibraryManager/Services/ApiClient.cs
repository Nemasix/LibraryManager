using System.Net.Http.Headers;

namespace WebAppLibraryManager.Services
{
    public class ApiClient
    {
        private readonly HttpClient HttpClient;
        private IConfiguration Configuration;

        public ApiClient(IConfiguration configuration)
        {
            Configuration = configuration;
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(configuration.GetSection("ApiSettings").GetValue<string>("Url"));
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetAsync<T>(string path)
        {
            try
            {
                var response = await HttpClient.GetAsync($"api/{path}");
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
            var response = await HttpClient.DeleteAsync($"api/{path}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error calling the API: {response.ReasonPhrase}");
            }
        }

        public async Task<T> PostAsync<T>(string path, T item)
        {
            var response = await HttpClient.PostAsJsonAsync($"api/{path}", item);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                throw new Exception($"Error calling the API: {response.ReasonPhrase}");
            }
        }

        public async Task<T> PutAsync<T>(string path, T item)
        {
            var response = await HttpClient.PutAsJsonAsync($"api/{path}", item);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                throw new Exception($"Error calling the API: {response.ReasonPhrase}");
            }
        }

        public async Task<T> PatchAsync<T>(string path, T item)
        {
            var response = await HttpClient.PatchAsJsonAsync($"api/{path}", item);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                throw new Exception($"Error calling the API: {response.ReasonPhrase}");
            }
        }
    }
}
