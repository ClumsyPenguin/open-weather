using OpenWeather.Domain.Temperature.Models;

namespace OpenWeather.Adapters.REST.Temperature.Ports;

public interface ITemperatureService
{
    public Task<double> GetCurrentTemperature(GetCurrentTemperatureRequest request);
}