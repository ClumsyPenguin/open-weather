using Microsoft.Extensions.Caching.Hybrid;
using OpenWeather.Domain.Temperature.Models;
using OpenWeather.Domain.Temperature.Services.Ports;

namespace OpenWeather.Domain.Temperature.Services;

public class TemperatureService(HybridCache hybridCache) : ITemperatureService
{
    public async Task<double> GetCurrentTemperatureAsync(GetCurrentTemperatureRequest request,
        CancellationToken cancellationToken)
    {
        var entryOptions = new HybridCacheEntryOptions
        {
            Expiration = TimeSpan.FromHours(1), //KMI updates weather data at intervals of 1 hour
            LocalCacheExpiration = TimeSpan.FromHours(1),
        };
        
        return await hybridCache.GetOrCreateAsync(
            $"temperature-{request.Longitude}-{request.Latitude}",
            async _ => await Task.FromResult(21d),
            entryOptions,
            tags: ["temperature"],
            cancellationToken: cancellationToken
        );
    }
}