using OpenWeather.Adapters.REST.Temperature.Ports;

namespace OpenWeather.Domain.Temperature;

public class TemperatureService : ITemperatureService
{
    public Task<double> GetCurrentTemperature()
    {
        return Task.FromResult(1d);
    }
}