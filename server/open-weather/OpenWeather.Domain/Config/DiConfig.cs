using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace OpenWeather.Domain.Config
{
    public static class DiConfig
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            Temperature.Config.DiConfig.Configure(serviceCollection);

            serviceCollection.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);
        }
    }
}
