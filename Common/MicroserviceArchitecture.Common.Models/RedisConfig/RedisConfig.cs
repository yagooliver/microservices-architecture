using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace MicroserviceArchitecture.Common.Models.RedisConfig
{
    public static class RedisConfig
    {
        public static void RedisConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDistributedMemoryCache();
            var password = configuration["Redis:Password"];
            var host = configuration["Redis:Url"];
            var port = configuration["Redis:Port"];
            var config = new ConfigurationOptions
                {
                    EndPoints = { { host, Convert.ToInt32(port) } },
                    Password = string.IsNullOrEmpty(password) ? "" : password,
                    ConnectRetry = 3,
                    AbortOnConnectFail = false
                };

            services.AddStackExchangeRedisCache(options => { options.ConfigurationOptions = config; });
            services.AddScoped<IDatabase>(cfg =>
            {
                IConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect($"{config.ToString()}");
                return multiplexer.GetDatabase();
            });
            //IConnectionMultiplexer connection = ConnectionMultiplexer.Connect(config);
            //services.AddSingleton(connection);
            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.ConfigurationOptions = config;
            //    options.ConnectionMultiplexerFactory = () =>
            //    {
            //        return Task.FromResult(connection);
            //    };
            //});
        }
    }
}