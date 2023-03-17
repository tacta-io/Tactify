using Tacta.EventStore.Repository;
using Tactify.Core.ReadModels.Repositories;

namespace Tactify.Sql.Repositories
{
    public sealed class BoardReadModelRepository : ProjectionRepository, IBoardReadModelRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public BoardReadModelRepository(ISqlConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory, "BoardReadModel")
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
    }
}
