using System.Net;
using System.Net.Http.Json;
using OpenWeather.Aspects.Resiliency;
using OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.DTOs;
using OpenWeather.Core;
using OpenWeather.Core.Exceptions;

namespace OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.Services;

public interface IOpenMeteoClient
{
    public Task<GetCurrentTemperatureDTO> GetCurrentTemperature(GetCurrentTemperatureRequest request);
}

internal class OpenMeteoClient: IOpenMeteoClient
{
    private readonly HttpClient _httpClient;

    public OpenMeteoClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(Constants.OpenMeteoForecastApiBaseUrl);
    }

    [Resilient]
    public async Task<GetCurrentTemperatureDTO> GetCurrentTemperature(GetCurrentTemperatureRequest request)
    {
        var x = await _httpClient.GetAsync($"?latitude={request.Latitude}&longitude={request.Longitude}&current=temperature");
        //The tojson gave problems

        return new GetCurrentTemperatureDTO();
    }
}