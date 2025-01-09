using FluentValidation;
using OpenWeather.Domain.Temperature.Models;

namespace OpenWeather.Domain.Temperature.Validators;

public class TemperatureValidator : AbstractValidator<GetCurrentTemperatureRequest>
{
    public TemperatureValidator()
    {
        RuleFor(x => x.Longitude);
    }
}