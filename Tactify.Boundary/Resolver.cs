using Microsoft.Extensions.DependencyInjection;
using Tacta.EventStore.Projector;
using Tacta.EventStore.Repository;
using Tactify.Core.Boards.Repositories;
using Tactify.Core.Boards.Services;
using Tactify.Core.ReadModels.Projections;
using Tactify.Core.ReadModels.Repositories;
using Tactify.Sql;
using Tactify.Sql.Repositories;

namespace Tactify.Boundary
{
    public static class Resolver
    {
        public static void AddRepositories(this IServiceCollection services)
        {                  
            var connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Tactify;Integrated Security=true;TrustServerCertificate=True;";
            services.AddTransient<ISqlConnectionFactory>(sp => new SqlConnectionFactory(connectionString));
            services.AddTransient<IEventStoreRepository, EventStoreRepository>();           
            services.AddTransient<IBoardRepository, BoardRepository>();
            services.AddTransient<IBoardReadModelRepository, BoardReadModelRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IBoardService, BoardService>();
        }

        public static void AddProjections(this IServiceCollection services)
        {
            services.AddTransient<IProjectionProcessor, ProjectionProcessor>();
            services.AddTransient<IProjection, BoardReadModelProjection>();
        }
    }
}
