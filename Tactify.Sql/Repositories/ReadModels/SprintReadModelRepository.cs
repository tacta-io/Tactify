using Dapper;
using Tacta.EventStore.Repository;
using Tactify.Core.ReadModels.SprintReadModels;
using Tactify.Core.ReadModels.SprintReadModels.Repositories;

namespace Tactify.Sql.Repositories.ReadModels
{
    public sealed class SprintReadModelRepository : ProjectionRepository, ISprintReadModelRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private static readonly string _tableName = "[dbo].[SprintReadModel]";

        public SprintReadModelRepository(ISqlConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory, _tableName)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<SprintReadModel>> GetAsync(string boardId)
        {
            var select = $"SELECT * FROM {_tableName} WHERE [BoardId] = @BoardId";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            var args = new { BoardId = boardId };

            return await connection.QueryAsync<SprintReadModel>(select, args).ConfigureAwait(false);
        }

        public async Task OnSprintCreatedAsync(SprintReadModel sprintReadModel)
        {
            var insert =
                @$"INSERT INTO {_tableName} 
                VALUES (@BoardId, @SprintId, @Status, @CreatedAt, @StartedAt, @EndedAt, @Sequence)";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            await connection.ExecuteAsync(insert, sprintReadModel).ConfigureAwait(false);
        }

        public async Task OnSprintStartedAsync(SprintReadModel sprintReadModel)
        {
            var update =
                @$"UPDATE {_tableName} 
                SET [Status] = @Status, [StartedAt] = @StartedAt, [Sequence] = @Sequence
                WHERE [BoardId] = @BoardId AND [SprintId] = @SprintId";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            await connection.ExecuteAsync(update, sprintReadModel).ConfigureAwait(false);
        }

        public async Task OnSprintEndedAsync(SprintReadModel sprintReadModel)
        {
            var update =
                @$"UPDATE {_tableName} 
                SET [Status] = @Status, [EndedAt] = @EndedAt, [Sequence] = @Sequence
                WHERE [BoardId] = @BoardId AND [SprintId] = @SprintId";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            await connection.ExecuteAsync(update, sprintReadModel).ConfigureAwait(false);
        }
    }
}
