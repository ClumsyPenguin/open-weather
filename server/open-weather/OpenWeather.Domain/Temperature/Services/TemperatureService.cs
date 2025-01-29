using OpenWeather.Aspects.Caching;
using OpenWeather.Domain.Temperature.Models;
using OpenWeather.Domain.Temperature.Ports;
using OpenWeather.Domain.Temperature.Services.Clients;
using OpenWeather.Domain.Temperature.Services.Ports;

namespace OpenWeather.Domain.Temperature.Services;

internal class TemperatureService(ITemperatureRepository temperatureRepository, IAzureFunctionService azureFunctionService) : ITemperatureService
{
    //KMI updates weather data at intervals of 1 hour
    [Cache(60)]
    public async Task<CurrentTemperature> GetCurrentTemperatureAsync(GetCurrentTemperatureRequest request, CancellationToken cancellationToken)
    {
        return await GetCurrentTemperatureFromDbOrExternal(request.Longitude, request.Latitude);
    }

    private async Task<CurrentTemperature> GetCurrentTemperatureFromDbOrExternal(double longitude, double latitude)
    {
        var temperatureInDb = await temperatureRepository.GetCurrentTemperatureAsync(longitude, latitude);
        return temperatureInDb ?? await azureFunctionService.GetCurrentTemperatureAsync(longitude, latitude);
        
        //TODO persist the result of the azure function call
    }
}