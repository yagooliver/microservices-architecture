using Microsoft.Extensions.Configuration;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
    .AddJsonFile($"{AppContext.BaseDirectory}/Ocelot/ocelot.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables();

builder.Configuration.AddOcelotWithSwaggerSupport((o) =>
{
    o.FileOfSwaggerEndPoints = "ocelot.swagger";
    o.Folder = "Ocelot";
    o.HostEnvironment = builder.Environment;
    o.PrimaryOcelotConfigFileName = $"{AppContext.BaseDirectory}/Ocelot/ocelot.{builder.Environment.EnvironmentName}.json";
});

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxConcurrentConnections = 100;
    serverOptions.ConfigureEndpointDefaults(lo => lo.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddOcelot(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
});

//app.UseHttpsRedirection();
app.UseRouting();
app.UseOcelot();

app.Run();