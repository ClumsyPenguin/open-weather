using System.Net.Http.Json;
using OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.DTOs;

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

    public async Task<GetCurrentTemperatureDTO> GetCurrentTemperature(GetCurrentTemperatureRequest request) 
        => await _httpClient.GetFromJsonAsync<GetCurrentTemperatureDTO>($"?latitude={request.Latitude}&longitude={request.Longitude}&current=temperature") ?? new GetCurrentTemperatureDTO();
}