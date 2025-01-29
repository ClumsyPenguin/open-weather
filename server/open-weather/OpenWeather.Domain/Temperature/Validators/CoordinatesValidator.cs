using FluentValidation;
using OpenWeather.Domain.Temperature.Models;

namespace OpenWeather.Domain.Temperature.Validators;

public class CoordinatesValidator : AbstractValidator<GetCurrentTemperatureRequest>
{
    public CoordinatesValidator()
    {
        RuleFor(request => request.Latitude)
            .NotEmpty()
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90.");

        RuleFor(request => request.Longitude)
            .NotEmpty()
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180.");    
    }
}