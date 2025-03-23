using OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.Clients;
using OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.DTOs;
using Polly.CircuitBreaker;

namespace OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.Services
{
    internal interface IOpenMeteoService
    {
        Task<GetCurrentTemperatureDTO> GetCurrentTemperature(GetCurrentTemperatureRequest request);
    }

    internal class OpenMeteoService(IOpenMeteoClient openMeteoClient) : IOpenMeteoService
    {
        public Task<GetCurrentTemperatureDTO> GetCurrentTemperature(GetCurrentTemperatureRequest request)
        {
            return openMeteoClient.GetCurrentTemperature(request);
           
        }
    }
}
