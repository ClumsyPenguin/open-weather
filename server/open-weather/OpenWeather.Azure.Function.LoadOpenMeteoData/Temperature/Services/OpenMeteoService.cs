using OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.DTOs;

namespace OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.Services
{
    public interface IOpenMeteoService
    {
        Task<GetCurrentTemperatureDTO> GetCurrentTemperature(GetCurrentTemperatureRequest request);
    }

    internal class OpenMeteoService(IOpenMeteoClient openMeteoClient) : IOpenMeteoService
    {
        public async Task<GetCurrentTemperatureDTO> GetCurrentTemperature(GetCurrentTemperatureRequest request)
        {
            var x =  await openMeteoClient.GetCurrentTemperature(request);

            return x;
        }
    }
}
