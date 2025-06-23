var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Foosball_ApiService>("apiservice");

builder.AddProject<Projects.Foosball_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
