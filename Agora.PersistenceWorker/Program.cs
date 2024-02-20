using Agora.PersistenceWorker;
using Agora.Simulator;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddGrpcClient<Market.MarketClient>(o =>
{
    o.Address = new Uri(builder.Configuration.GetValue<string>("Market:ServerAddress"));
    //o.Address = new Uri("https://agora-simulator");
});

builder.AddServiceDefaults();

var host = builder.Build();
host.Run();