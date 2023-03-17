using Tactify.Boundary;
using Tactify.Projections;

IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddHostedService<Worker>();

    services.AddProjections();
    services.AddRepositories();

}).Build();

host.Run();
