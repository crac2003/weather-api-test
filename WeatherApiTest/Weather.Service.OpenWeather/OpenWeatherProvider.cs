using System;
using System.Threading.Tasks;
using Weather.Domain.Http;
using Weather.Domain.Models;
using Weather.Domain.Services;

namespace Weather.Service.OpenWeather
{
    public class OpenWeatherProvider : IWeatherDataProvider
    {
        private readonly IWeatherHttpClient _httpClient;
        private readonly IOpenWeatherConfig _config;

        public OpenWeatherProvider(IWeatherHttpClient httpClient, IOpenWeatherConfig config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public Task<City> GetCityForecastAsync(int id)
        {
            return _httpClient.GetAsync<City>($"{_config.OpenWeatherUrl}?id={id}&appid={_config.OpenWeatherKey}");
        }
    }
}
