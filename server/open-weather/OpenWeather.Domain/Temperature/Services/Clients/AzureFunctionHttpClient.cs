using System.Net.Http.Json;
using OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.DTOs;
using OpenWeather.Domain.Temperature.Models;

namespace OpenWeather.Domain.Temperature.Services.Clients;

public class AzureFunctionHttpClient : IAzureFunctionService
{
    private readonly HttpClient _httpClient;
    public AzureFunctionHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:7062/api/");
    }
    
    public async Task<CurrentTemperature> GetCurrentTemperatureAsync(double latitude, double longitude)
    {
        //TODO error-handling
        var result = await _httpClient.GetFromJsonAsync<GetCurrentTemperatureDTO>($"get-current-temperature?latitude={latitude}&longitude={longitude}");

        if(result is null)
            return new CurrentTemperature();
        
        return new CurrentTemperature
        {
            Longitude = result.Longitude,
            Latitude = result.Latitude,
            
            TemperatureC = result.Current.Temperature,
            
            TimeStamp = DateTime.Parse(result.Current.Time)
        };
    }
}