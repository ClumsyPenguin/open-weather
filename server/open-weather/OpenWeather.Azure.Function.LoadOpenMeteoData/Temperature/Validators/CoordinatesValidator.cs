using FluentValidation;
using OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.DTOs;

namespace OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.Validators;

public class CoordinatesValidator : AbstractValidator<GetCurrentTemperatureRequest>
{
    //Duplicate from the validator in the domain, it might be worthwhile to make same shared
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