using System.Reflection;

namespace OpenWeather.Azure.Function.LoadOpenMeteoData;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}