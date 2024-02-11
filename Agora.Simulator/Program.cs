using Agora.Simulator;
using Agora.Simulator.Services;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics.AddMeter("MarketSimulator");
        metrics.AddPrometheusExporter();
    });

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSingleton<MarketSimulatorMetrics>();
builder.Services.AddSingleton<OrderGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.Services.GetService<OrderGenerator>()?.Start();
app.UseHealthChecksPrometheusExporter("/metrics");
app.MapDefaultEndpoints();
app.MapGrpcService<MarketService>();

app.Run();