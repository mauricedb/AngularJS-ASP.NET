using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace System.Web.Http
{
    public static class HttpConfigurationExtensions
    {
        public static void UseCamelCasePropertyNames(this HttpConfiguration configuration)
        {
            configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver
                = new CamelCasePropertyNamesContractResolver();
        }
    }
}
