using FluentValidation;
using OpenWeather.Domain.Temperature.Models;

namespace OpenWeather.Domain.Temperature.Validators;

public class TemperatureValidator : AbstractValidator<GetCurrentTemperatureRequest>
{
    public TemperatureValidator()
    {
        RuleFor(request => request.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90.");

        RuleFor(request => request.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180.");    
    }
}