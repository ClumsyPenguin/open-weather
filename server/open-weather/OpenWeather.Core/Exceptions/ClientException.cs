using System.Net;

namespace OpenWeather.Core.Exceptions
{
    [Serializable]
    public class ClientException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public ClientException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }       
    }
}