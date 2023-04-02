using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Yatt.HttpClientService
{
    public class YattHttpClient<T> : IYattHttpClient<T> where T : class
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public YattHttpClient(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<T> DeleteAsync(string url, string id)
        {
            var response = await _client.DeleteAsync($"{url}/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var resp = JsonSerializer.Deserialize<T>(content, _options);

            return resp;
        }

        public async Task<T> GetByIdAsync(string url, string id)
        {
            var response = await _client.GetAsync($"{url}/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var resp = JsonSerializer.Deserialize<T>(content, _options);

            return resp;
        }

        public async Task<T> PostAsync(string url, T item)
        {
            var content = JsonSerializer.Serialize(item);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _client.PostAsync(url, bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            var resp = JsonSerializer.Deserialize<T>(postContent, _options);
          
            return resp;
        }
    }
}
