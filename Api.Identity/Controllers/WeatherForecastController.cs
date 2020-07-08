using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Service.Identity;

namespace Api.Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IWeatherForecastService service;

        public WeatherForecastController(IWeatherForecastService service)
        {
            this.service = service;
        }

        private const string opCodeKey = "opCode";
        private const string opCodeValue = "getWeatherForecast";

        [HttpGet] 
        public IActionResult Get()
        { 
            var data = service.Get();
            return Ok(data);
        }
        
    }
}