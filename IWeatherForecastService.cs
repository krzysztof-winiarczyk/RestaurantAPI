using System.Collections.Generic;

namespace RestaurantAPI
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> GetForecast(int howMany, int minimalTemperature, int maximalTemperature);
    }
}