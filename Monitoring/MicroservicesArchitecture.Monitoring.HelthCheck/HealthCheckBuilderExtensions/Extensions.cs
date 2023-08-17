using Microsoft.Extensions.Diagnostics.HealthChecks;

using RabbitMQ.Client;

using TelenetBusiness.Safepoint.Healthcheck.Models;

namespace TelenetBusiness.Safepoint.Healthcheck.HealthCheckBuilderExtensions
{
    public static class Extensions
    {
        #region Public Methods

        public static IHealthChecksBuilder AddDatabaseChecks(this IHealthChecksBuilder builder, Settings settings)
        {
            if (settings != null && settings.DatabaseChecks != null && settings.DatabaseChecks.Any())
            {
                foreach (DatabaseCheck? dbCheck in settings.DatabaseChecks)
                {
                    builder.AddSqlServer(dbCheck.ConnectionString,
                    tags: new string[] { "db" }, name: dbCheck.Name);
                }
            }

            return builder;
        }

        public static IHealthChecksBuilder AddServicesChecks(this IHealthChecksBuilder builder, Settings settings)
        {
            if (settings != null && settings.UrlChecks != null && settings.UrlChecks.Any())
            {
                foreach (UrlCheck? urlCheck in settings.UrlChecks)
                {
                    builder.AddUrlGroup(
                           new Uri(urlCheck.Url),
                           name: urlCheck.Name,
                           failureStatus: HealthStatus.Unhealthy,
                           timeout: TimeSpan.FromSeconds(3),
                           tags: new string[] { urlCheck.Tags });

                    System.Console.WriteLine($"Added check {urlCheck.Url}");
                }
            }

            return builder;
        }


        public static IHealthChecksBuilder AddMessageBrokerCheck(this IHealthChecksBuilder builder, Settings settings)
        {
            List<string> tags = new() { "rabbit" };

            return builder.AddRabbitMQ(opts =>
            {
                var factory = new ConnectionFactory()
                {
                    Uri = new Uri(settings.EventBus.Hostname),
                    UserName = settings.EventBus.Username,
                    Password = settings.EventBus.Password,
                    AutomaticRecoveryEnabled = true
                };

                return factory.CreateConnection();
            }, name: "RabbitMQ", tags: tags);
        }

        public static IHealthChecksBuilder AddCacheCheck(this IHealthChecksBuilder builder, Settings settings)
        {
            return builder.AddRedis(settings.RedisConnectionString, "Redis", tags: new List<string>() { "redis" });
        }

        #endregion Public Methods
    }
}