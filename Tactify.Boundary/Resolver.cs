using Microsoft.Extensions.DependencyInjection;
using Tacta.EventStore.Projector;
using Tacta.EventStore.Repository;
using Tactify.Core.Boards.Repositories;
using Tactify.Core.Boards.Services;
using Tactify.Core.ReadModels.ActivityReadModels.Projections;
using Tactify.Core.ReadModels.ActivityReadModels.Repositories;
using Tactify.Core.ReadModels.ActivityReadModels.Services;
using Tactify.Core.ReadModels.BoardReadModels.Projections;
using Tactify.Core.ReadModels.BoardReadModels.Repositories;
using Tactify.Core.ReadModels.BoardReadModels.Services;
using Tactify.Core.ReadModels.SprintReadModels.Projections;
using Tactify.Core.ReadModels.SprintReadModels.Repositories;
using Tactify.Core.ReadModels.SprintReadModels.Services;
using Tactify.Core.ReadModels.TicketReadModels.Projections;
using Tactify.Core.ReadModels.TicketReadModels.Repositories;
using Tactify.Core.ReadModels.TicketReadModels.Services;
using Tactify.Core.Tickets.Repositories;
using Tactify.Core.Tickets.Services;
using Tactify.Sql;
using Tactify.Sql.Repositories;
using Tactify.Sql.Repositories.ReadModels;

namespace Tactify.Boundary
{
    public static class Resolver
    {  

        public static void AddEventStore(this IServiceCollection services)
        {
            var connectionString =
                "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Tactify;Integrated Security=true;TrustServerCertificate=True;";

            services.AddTransient<ISqlConnectionFactory>(sp => new SqlConnectionFactory(connectionString));
            services.AddTransient<IEventStoreRepository, EventStoreRepository>();
            services.AddTransient<IBoardRepository, BoardRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();
        }

        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<IBoardService, BoardService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IBoardReadModelService, BoardReadModelService>();
            services.AddTransient<ISprintReadModelService, SprintReadModelService>();
            services.AddTransient<ITicketReadModelService, TicketReadModelService>();
            services.AddTransient<IActivityReadModelService, ActivityReadModelService>();
        }

        public static void AddReadModelRepositories(this IServiceCollection services)
        {
            services.AddTransient<IBoardReadModelRepository, BoardReadModelRepository>();
            services.AddTransient<ISprintReadModelRepository, SprintReadModelRepository>();
            services.AddTransient<ITicketReadModelRepository, TicketReadModelRepository>();
            services.AddTransient<IActivityReadModelRepository, ActivityReadModelRepository>();
        }

        public static void AddReadModelProjections(this IServiceCollection services)
        {
            services.AddSingleton<IProjectionProcessor, ProjectionProcessor>();
            services.AddTransient<IProjection, BoardReadModelProjection>();
            services.AddTransient<IProjection, SprintReadModelProjection>();
            services.AddTransient<IProjection, TicketReadModelProjection>();
            services.AddTransient<IProjection, ActivityReadModelProjection>();
        }
    }
}
