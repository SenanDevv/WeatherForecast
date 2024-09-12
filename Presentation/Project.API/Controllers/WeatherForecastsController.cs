using Microsoft.AspNetCore.Mvc;
using Project.Service.Services.Abstraction;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastsController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastsController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] DateTime? date, [FromQuery] string? city, [FromQuery] string? country)
        {
            return Ok(await _weatherForecastService.GetForecasts(date, city, country));
        }
    }
}
