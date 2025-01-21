using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace OpenWeather.Domain.Config
{
    public static class DiConfigLoader
    {
        public static void Load(IServiceCollection serviceCollection)
        {
            Temperature.Config.DiConfig.Configure(serviceCollection);

            serviceCollection.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);
        }
    }
}
