using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Weather.Domain.Http
{
    public class WeatherHttpClient : IWeatherHttpClient
    {
        private static HttpClient _client;

        public WeatherHttpClient(HttpClientHandler handler = null)
        {
            _client = new HttpClient(handler ?? new HttpClientHandler());
        }

        public async Task<TResult> GetAsync<TResult>(string url)
        {
            var result = await _client.GetAsync(url);
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResult>(content);
        }
    }
}
