using Tactify.Boundary;
using Tactify.Projections;

IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddHostedService<Worker>();

    services.AddEventStore();
    services.AddReadModelRepositories();
    services.AddReadModelProjections();

}).Build();

host.Run();
