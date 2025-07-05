using Foosball.Application.Services;
using Foosball.Infrastructure;
using Foosball.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddSqlServerClient(connectionName: "sql");
builder.AddSqlServerDbContext<FoosballDbContext>("database");

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddControllers();

builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IMatchRepository, MatchRepository>();
builder.Services.AddScoped<IGoalRepository, GoalRepository>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FoosballDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        List<ScalarServer> servers = [];

        string? httpsPort = Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_PORT");
        if (httpsPort is not null)
        {
            servers.Add(new ScalarServer($"https://localhost:{httpsPort}"));
        }

        string? httpPort = Environment.GetEnvironmentVariable("ASPNETCORE_HTTP_PORT");
        if (httpPort is not null)
        {
            servers.Add(new ScalarServer($"http://localhost:{httpPort}"));
        }

        options.Servers = servers;
        options.Title = "Foosball API";
        options.ShowSidebar = true;
    });
}

app.MapDefaultEndpoints();

app.MapControllers();

app.Run();
