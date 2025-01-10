using OpenWeather.Adapters.REST.Temperature.Ports;
using OpenWeather.Domain.Temperature.Models;

namespace OpenWeather.Domain.Temperature;

public class TemperatureService : ITemperatureService
{
    public Task<double> GetCurrentTemperature(GetCurrentTemperatureRequest request)
    {
        return Task.FromResult(1d);
    }
}