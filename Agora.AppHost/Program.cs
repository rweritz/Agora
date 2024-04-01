using System.Net.Sockets;

var builder = DistributedApplication.CreateBuilder(args);

var simulator = builder.AddProject<Projects.Agora_Simulator>("agora-simulator")
    .ExcludeLaunchProfile()
    .WithEndpoint("gRPC", e =>
    {
        e.IsProxied = false;
        e.Port = 5223;
    })
    .WithEndpoint("Http", e =>
    {
        e.IsProxied = false;
        e.Port = 5000;
    });

//var postgresdb = builder.AddPostgres("pg")
//    .AddDatabase("postgresdb");

builder.AddProject<Projects.Agora_PersistenceWorker>("agora-persistence").WithReference(simulator);

builder.Build().Run();
