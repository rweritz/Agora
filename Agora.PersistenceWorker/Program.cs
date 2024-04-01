using Agora.PersistenceWorker;
using Agora.Simulator;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContext<MarketDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MarketDbContext")));
builder.Services.AddHostedService<Worker>();
builder.Services.AddGrpcClient<Market.MarketClient>(o =>
{
    o.Address = new Uri(builder.Configuration.GetValue<string>("Market:ServerAddress"));
});

builder.AddServiceDefaults();

var host = builder.Build();
host.Run();