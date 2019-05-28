using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weather.Domain.Models.Requests;
using Weather.Domain.Services;

namespace WeatherApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        // GET api/weather?cities=id,id1,
        [HttpGet]
        public async Task<IActionResult> Get(string cities)
        {
            if (string.IsNullOrWhiteSpace(cities))
            {
                return BadRequest();
            }

            var requests = cities.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries).Select(x => GetCityRequest.Create(int.Parse(x)));
            var result = await _weatherService.GetAsync(requests);
            return Ok(result);
        } 
    }
}
