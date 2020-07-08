using System.Collections.Generic;

namespace Service.Identity
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get();
    }
}