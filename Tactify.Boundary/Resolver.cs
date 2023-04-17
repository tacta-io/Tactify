using Microsoft.Extensions.DependencyInjection;
using Tacta.EventStore.Projector;
using Tacta.EventStore.Repository;
using Tactify.Core.Boards.Repositories;
using Tactify.Core.Boards.Services;
using Tactify.Core.ReadModels.ActivityReadModels.Projections;
using Tactify.Core.ReadModels.ActivityReadModels.Repositories;
using Tactify.Core.ReadModels.BoardReadModels.Projections;
using Tactify.Core.ReadModels.BoardReadModels.Repositories;
using Tactify.Core.ReadModels.BoardReadModels.Services;
using Tactify.Core.ReadModels.SprintReadModels.Projections;
using Tactify.Core.ReadModels.SprintReadModels.Repositories;
using Tactify.Core.ReadModels.SprintReadModels.Services;
using Tactify.Sql;
using Tactify.Sql.Repositories;

namespace Tactify.Boundary
{
    public static class Resolver
    {
        private static readonly string connectionString =
            "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Tactify;Integrated Security=true;TrustServerCertificate=True;";

        public static void AddEventStore(this IServiceCollection services)
        {
            services.AddTransient<ISqlConnectionFactory>(sp => new SqlConnectionFactory(connectionString));
            services.AddTransient<IEventStoreRepository, EventStoreRepository>();
            services.AddTransient<IProjectionProcessor, ProjectionProcessor>();
        }

        public static void AddBoards(this IServiceCollection services)
        { 
            services.AddTransient<IBoardRepository, BoardRepository>();
            services.AddTransient<IBoardService, BoardService>();
        }

        public static void AddBoardReadModels(this IServiceCollection services)
        {
            services.AddTransient<IBoardReadModelRepository, BoardReadModelRepository>();
            services.AddTransient<IBoardReadModelService, BoardReadModelService>();
            services.AddTransient<IProjection, BoardReadModelProjection>();
        }

        public static void AddSprintReadModels(this IServiceCollection services)
        {
            services.AddTransient<ISprintReadModelRepository, SprintReadModelRepository>();
            services.AddTransient<ISprintReadModelService, SprintReadModelService>();
            services.AddTransient<IProjection, SprintReadModelProjection>();
        }

        public static void AddActivityReadModels(this IServiceCollection services)
        {
            services.AddTransient<IActivityReadModelRepository, ActivityReadModelRepository>();
            services.AddTransient<IProjection, ActivityReadModelProjection>();
        }
    }
}
