using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Weather.Domain.Http
{
    public class WeatherHttpClient : IWeatherHttpClient
    {
        private static HttpClient _client;

        public WeatherHttpClient()
        {
            _client = new HttpClient();
        }

        public async Task<TResult> GetAsync<TResult>(string url)
        {
            var result = await _client.GetAsync(url);
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResult>(content);
        }
    }
}
