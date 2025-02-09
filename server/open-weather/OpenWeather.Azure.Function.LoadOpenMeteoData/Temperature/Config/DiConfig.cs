using Autofac;
using OpenWeather.Domain.Temperature.Ports;
using OpenWeather.Domain.Temperature.Services.Clients;

namespace OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.Config;

public class TemperatureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AzureFunctionTemperatureService>().As<IAzureFunctionTemperatureService>();
    }
}