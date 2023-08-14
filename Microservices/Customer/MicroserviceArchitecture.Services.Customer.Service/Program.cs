using MicroserviceArchitecture.Services.Customer.Service;
using MicroserviceArchitecture.Services.Customer.Infra.SqlServer.ContextDb;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MicroserviceArchitecture.Services.Customer.Service.Config;
using MicroserviceArchitecture.Common.Models.RedisConfig;
using Microsoft.Extensions.Options;
using Google.Protobuf.WellKnownTypes;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxConcurrentConnections = 100;
    serverOptions.ConfigureEndpointDefaults(lo => lo.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2);
});

builder.Services.AddControllers();

Console.WriteLine(builder.Environment.EnvironmentName);
Console.WriteLine(builder.Configuration.GetConnectionString("ServiceDb"));
builder.Services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ServiceDb")));

builder.Services.RedisConfiguration(builder.Configuration);

builder.Services.AddRepositories();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerSetup();

//Inject dependencies
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddCommands();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

app.EnsureMigrationOfContext<CustomerDbContext>();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
