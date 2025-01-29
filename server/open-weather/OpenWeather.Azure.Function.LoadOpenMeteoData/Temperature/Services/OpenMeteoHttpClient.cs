using System.Net.Http.Json;
using OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.DTOs;

namespace OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.Services;

public interface IOpenMeteoService
{
    public Task<GetCurrentTemperatureDTO> GetCurrentTemperature(GetCurrentTemperatureRequest request);
}

internal class OpenMeteoHttpClient: IOpenMeteoService
{
    private readonly HttpClient _httpClient;

    public OpenMeteoHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(Constants.OpenMeteoForecastApiBaseUrl);
    }

    public async Task<GetCurrentTemperatureDTO> GetCurrentTemperature(GetCurrentTemperatureRequest request) 
        => await _httpClient.GetFromJsonAsync<GetCurrentTemperatureDTO>($"?latitude={request.Latitude}&longitude={request.Longitude}&current=temperature") ?? new GetCurrentTemperatureDTO();
}