using System.Diagnostics;
using Agora.Simulator;
using Agora.Simulator.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("/run/secrets/secrets_file", true);

var additionalMeters = new[] { "Agora.Simulator" };
builder.AddServiceDefaults(additionalMeters);
builder.Services.AddOpenTelemetry();

builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.ListenAnyIP(5223, listenOptions =>
    {
        //listenOptions.Protocols = HttpProtocols.Http3;
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
        //listenOptions.UseHttps(@"C:\Users\excri\.aspnet\https\Agora.Simulator.pfx", "daikav48dfio593");
        listenOptions.UseHttps();
    });
});

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSingleton<MarketSimulatorMetrics>();
builder.Services.AddHostedService<OrderGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapDefaultEndpoints();
app.MapGrpcService<MarketService>();

app.Run();