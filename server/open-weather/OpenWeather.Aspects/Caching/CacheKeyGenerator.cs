using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace OpenWeather.Aspects.Caching
{
    public static class CacheKeyGenerator
    {
        public static string Generate(MethodInfo methodInfo, object[] parameterValues)
        {
            return CreateCacheString(methodInfo, parameterValues);
        }

        private static string CreateCacheString(MethodInfo methodInfo, object[] parameterValues)
        {
            // This caching will fail if the method is created in a module (as we expect DeclaringType to be non-null)
            // https://stackoverflow.com/questions/35266010/can-propertyinfo-declaringtype-really-ever-be-null
            var type = methodInfo.DeclaringType!.ToString();
            var methodName = methodInfo.Name;
            var paramValues = SerializeAndShortenParameterValues(parameterValues, 25);


            return string.Join(':',
                               type,
                               methodName,
                               paramValues);
        }

        private static string SerializeAndShortenParameterValues(object[] parameterValues, int maxParameterLength)
        {
            var paramValues = parameterValues.Where(p => p is not CancellationToken)
                                             .Select(p => p.GetType().IsClass ? JsonSerializer.Serialize(p) : p.ToString());

            var shortenedValues = ConditionallyShorten(paramValues, maxParameterLength);
            return string.Join('_', shortenedValues);
        }

        private static IEnumerable<string?> ConditionallyShorten(IEnumerable<string?> arr, int maxLength)
        {
            foreach (var value in arr)
            {
                if (value is null || value.Length <= maxLength)
                {
                    yield return value;
                }
                else
                {
                    yield return Convert.ToBase64String(GetHashedKey(value));
                }
            }
        }

        private static byte[] GetHashedKey(string inputString)
        {
            // There is only a negligible chance of having a collision:
            // https://stackoverflow.com/questions/4014090/is-it-safe-to-ignore-the-possibility-of-sha-collisions-in-practice
            using HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
    }
}
