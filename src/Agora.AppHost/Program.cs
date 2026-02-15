using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var simulator = builder.AddProject<Projects.Agora_Simulator>("agora-simulator")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
    .WithEndpoint("gRPC", e =>
    {
        e.IsProxied = false;
        e.Port = 5223;
    })
    .WithEndpoint("https", e =>
    {
        e.IsProxied = false;
        e.Port = 5001;
    });

//var postgresdb = builder.AddPostgres("pg")
//    .AddDatabase("postgresdb");

builder.AddProject<Projects.Agora_PersistenceWorker>("agora-persistence")
    .WithReference(simulator);

builder.Build().Run();
