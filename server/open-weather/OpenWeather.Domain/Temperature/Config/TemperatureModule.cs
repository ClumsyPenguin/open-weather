using OpenWeather.Domain.Temperature.Services.Ports;
using OpenWeather.Domain.Temperature.Services;
using Autofac;
using OpenWeather.Core.Extensions;
using OpenWeather.Aspects.Caching;

namespace OpenWeather.Domain.Temperature.Config
{
    internal class TemperatureModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterTypeWithInterception<TemperatureService,ITemperatureService>(typeof(ICacheInterceptor));          
        }        
    }
}
