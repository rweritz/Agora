var builder = DistributedApplication.CreateBuilder(args);

var simulator = builder.AddProject<Projects.Agora_Simulator>("agora-simulator")
    .WithLaunchProfile("https");

//var postgresdb = builder.AddPostgres("pg")
//    .AddDatabase("postgresdb");

builder.AddProject<Projects.Agora_PersistenceWorker>("agora-persistence");
//    .WithReference(simulator);

builder.Build().Run();
