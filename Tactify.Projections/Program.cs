using Tactify.Boundary;
using Tactify.Projections;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<Worker>();
builder.Services.AddWindowsService();
builder.Services.AddControllers();
builder.Services.AddEventStore();
builder.Services.AddReadModelRepositories();
builder.Services.AddReadModelProjections();

var host = builder.Build();

host.MapControllers();

host.Run();