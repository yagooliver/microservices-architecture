using System.Net;

using HealthChecks.UI.Client;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TelenetBusiness.Safepoint.Healthcheck.HealthCheckBuilderExtensions;
using TelenetBusiness.Safepoint.Healthcheck.Models;
using TelenetBusiness.Safepoint.Healthcheck.Writers;

//set ContentRootPath so that builder.Host.UseWindowsService() doesn't crash when running as a service
WebApplicationOptions webApplicationOptions = new WebApplicationOptions
{
    ContentRootPath = AppContext.BaseDirectory,
    Args = args
};
WebApplicationBuilder builder = WebApplication.CreateBuilder(webApplicationOptions);

// Add services to the container.
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.json")
    .AddEnvironmentVariables();
//adding health check services to container
Settings settings = builder.Configuration.GetSection("Settings").Get<Settings>();
HealthChecksUI healthChecksUI = builder.Configuration.GetSection("HealthChecksUI").Get<HealthChecksUI>();

builder.Services.AddHealthChecks()
                .AddDatabaseChecks(settings)
                .AddServicesChecks(settings)
                .AddMessageBrokerCheck(settings)
                .AddCacheCheck(settings);

//adding healthchecks UI
builder.Services.AddHealthChecksUI(opt => {
    opt.SetEvaluationTimeInSeconds(5); //time in seconds between check
    opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks
    opt.SetApiMaxActiveRequests(1); //api requests concurrency
    //opt.AddHealthCheckEndpoint("Status", settings.StatusUri);
    opt.SetHeaderText("Microservices - Application - Status");
})
.AddInMemoryStorage();

builder.Host.UseWindowsService();

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();

app.UseEndpoints(endpoints => {
    //adding endpoint of health check for the health check ui in UI format
    List<Tuple<string, string>> hcPoints = new();

    hcPoints.Add(new Tuple<string, string>("hc-rabbit", "rabbit"));
    hcPoints.Add(new Tuple<string, string>("hc-redis", "redis"));
    hcPoints.Add(new Tuple<string, string>("hc-services", "services"));
    hcPoints.Add(new Tuple<string, string>("hc-api-gateway", "gateway"));
    hcPoints.Add(new Tuple<string, string>("hc-db", "db"));

    foreach (Tuple<string, string>? item in hcPoints)
    {
        endpoints.MapHealthChecks($"/{item.Item1}", new HealthCheckOptions
        {
            Predicate = x => x.Tags.Contains(item.Item2),
            AllowCachingResponses = false,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
    }

    endpoints.MapHealthChecks($"/status", new HealthCheckOptions
    {
        AllowCachingResponses = false,
        //ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        ResultStatusCodes = new Dictionary<HealthStatus, int>() {
                { HealthStatus.Unhealthy, (int)HttpStatusCode.ServiceUnavailable },
                { HealthStatus.Healthy, (int)HttpStatusCode.OK },
                { HealthStatus.Degraded, (int)HttpStatusCode.InternalServerError }
            },
        ResponseWriter = JsonWriters.WriteDetailedResponse
    });

    //map healthcheck ui endpoing - default is /healthchecks-ui/
    endpoints.MapHealthChecksUI(opt => {
        opt.UIPath = "/dashboard";
    });
});

app.Run();