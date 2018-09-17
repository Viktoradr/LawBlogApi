using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LawBlog.Utilities.Api
{
    public class ResponseHelpers
    {
        public static HttpResponseMessage ResponseAPI<T>(T obj, HttpResponseMessage retorno)
        {
            var json = JsonConvert.SerializeObject(obj,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            retorno.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return retorno;
        }

        public static void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            errorArgs.ErrorContext.Handled = true;
        }
    }
}
