using System.Net.Mime;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using RabbitMQ.Client;

using TelenetBusiness.Safepoint.Healthcheck.Models;

namespace TelenetBusiness.Safepoint.Healthcheck.Writers
{
    public static class JsonWriters
    {
        #region Public Methods

        /// <summary>
        /// Creates a custom JSON response for the status endpoint
        /// </summary>
        /// <param name="context"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        public static Task WriteDetailedResponse(HttpContext context, HealthReport report)
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            string json = JsonSerializer.Serialize(
                new {
                    Status = report.Status.ToString(),
                    Info = report.Entries
                        .Select(e =>
                            new {
                                Key = e.Key,
                                Status = Enum.GetName(
                                    typeof(HealthStatus),
                                    e.Value.Status),
                                Error = e.Value.Exception?.Message,
                            })
                        .ToList()
                },
                jsonSerializerOptions);

            context.Response.ContentType = MediaTypeNames.Application.Json;
            return context.Response.WriteAsync(json);
        }

        #endregion Public Methods
    }
}