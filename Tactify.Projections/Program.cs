using Tactify.Boundary;
using Tactify.Projections;

Host.CreateDefaultBuilder(args).UseWindowsService().ConfigureServices(services =>
{
    services.AddHostedService<Worker>();

    services.AddEventStore();
    services.AddReadModelRepositories();
    services.AddReadModelProjections();

}).Build().Run();