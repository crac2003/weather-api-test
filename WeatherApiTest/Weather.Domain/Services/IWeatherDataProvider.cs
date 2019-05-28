using System.Threading.Tasks;
using Weather.Domain.Models;

namespace Weather.Domain.Services
{
    public interface IWeatherDataProvider
    {
        Task<City> GetCityForecastAsync(int id);
    }
}
