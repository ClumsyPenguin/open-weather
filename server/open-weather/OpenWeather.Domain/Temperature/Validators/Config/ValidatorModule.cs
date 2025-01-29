using Autofac;
using FluentValidation;
using OpenWeather.Domain.Temperature.Models;

namespace OpenWeather.Domain.Temperature.Validators.Config
{
    internal class ValidatorModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CoordinatesValidator>().As<IValidator<GetCurrentTemperatureRequest>>();
        }
    }
}
