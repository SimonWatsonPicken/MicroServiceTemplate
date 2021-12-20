using System;
using System.Net.Http;
using System.Text.Json;

namespace MicroService.Domain.Extensions
{
    public static class HttpExtensions
    {
        public static T GetResponseObject<T>(this HttpResponseMessage httpResponseMessage, out string error)
        {
            string responseContent = null;
            try
            {
                responseContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
                error = null;

                return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions { WriteIndented = true });
            }
            catch (Exception e)
            {
                error = responseContent == null
                    ? "Failed to read the content from the HTTP response message."
                    : $"Failed to deserialise the following content into an instance of the {typeof(T).Name} class.{Environment.NewLine}{responseContent}";

                if (httpResponseMessage != null)
                {
                    error +=
                        $"{Environment.NewLine}HTTP Status: {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase ?? "Null"}";
                }

                error += $"{Environment.NewLine}{e.Message}";

                return default;
            }
        }
    }
}