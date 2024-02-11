var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Agora_Simulator>("agora-simulator");

//builder.AddProject<Projects.MarketDemo_Web>("market-read-service")
//    .WithReference(apiService);

builder.Build().Run();
