using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Domain.Models;
using Weather.Domain.Models.Requests;

namespace Weather.Domain.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherDataProvider _weatherDataProvider;
        private readonly IBackupService _backupService;

        public WeatherService(IWeatherDataProvider weatherDataProvider, IBackupService backupService)
        {
            _weatherDataProvider = weatherDataProvider;
            _backupService = backupService;
        }

        public async Task<IEnumerable<City>> GetAsync(IEnumerable<GetCityRequest> requests)
        {
            var result = await Task.WhenAll(requests.Select(GetOrPullCityAsync));
            return result;
        }

        private async Task<City> GetOrPullCityAsync(GetCityRequest request)
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var id = $"city_{request.Id}_{date}.json";
            // get cached 
            var city = await _backupService.ReadAsync<City>(id);
            if (city != null)
            {
                return city;
            }

            city = await _weatherDataProvider.GetCityForecastAsync(request.Id);

            // save cache
            await _backupService.SaveAsync(id, city);

            return city;
        }
    }
}