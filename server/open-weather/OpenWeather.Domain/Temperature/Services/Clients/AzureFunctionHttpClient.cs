using System.Net.Http.Json;
using OpenWeather.Domain.Temperature.Models;

namespace OpenWeather.Domain.Temperature.Services.Clients;

public class AzureFunctionHttpClient : IAzureFunctionService
{
    private readonly HttpClient _httpClient;

    public AzureFunctionHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:7062/api");
    }
    
    public async Task<CurrentTemperature> GetCurrentTemperatureAsync(double latitude, double longitude)
    {
        //TODO error-handling
        return await _httpClient.GetFromJsonAsync<CurrentTemperature>($"get-current-temperature?latitude={latitude}&longitude={longitude}");
    }
}