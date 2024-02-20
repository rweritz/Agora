using Agora.Simulator;
using Agora.Simulator.Services;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddKeyPerFile("/run/secrets/");

var additionalMeters = new[] { "Agora.Simulator" };
builder.AddServiceDefaults(additionalMeters);
builder.Services.AddOpenTelemetry();

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSingleton<MarketSimulatorMetrics>();
builder.Services.AddHostedService<OrderGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapDefaultEndpoints();
app.MapGrpcService<MarketService>();

app.Run();