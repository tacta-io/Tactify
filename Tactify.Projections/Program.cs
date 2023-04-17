using Tactify.Boundary;
using Tactify.Projections;

IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddHostedService<Worker>();

    services.AddEventStore();
    services.AddBoardReadModels();
    services.AddSprintReadModels();
    services.AddActivityReadModels();

}).Build();

host.Run();
