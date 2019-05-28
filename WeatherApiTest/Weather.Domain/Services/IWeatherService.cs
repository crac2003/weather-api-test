using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Models;
using Weather.Domain.Models.Requests;

namespace Weather.Domain.Services
{
    public interface IWeatherService
    {
        Task<IEnumerable<City>> GetAsync(IEnumerable<GetCityRequest> requests);
    }
}
