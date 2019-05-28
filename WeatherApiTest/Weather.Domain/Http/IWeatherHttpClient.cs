using System.Threading.Tasks;

namespace Weather.Domain.Http
{
    public interface IWeatherHttpClient
    {
        Task<TResult> GetAsync<TResult>(string url);
    }
}