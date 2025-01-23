using Microsoft.Extensions.DependencyInjection;
using OpenWeather.Domain.Temperature.Services.Ports;
using OpenWeather.Domain.Temperature.Services;
using Autofac;

namespace OpenWeather.Domain.Temperature.Config
{
    internal class TemperatureModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TemperatureService>().As<ITemperatureService>();          
        }        
    }
}
