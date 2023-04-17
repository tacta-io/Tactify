using Dapper;
using Tacta.EventStore.Repository;
using Tactify.Core.ReadModels.SprintReadModels;
using Tactify.Core.ReadModels.SprintReadModels.Repositories;

namespace Tactify.Sql.Repositories
{
    public sealed class SprintReadModelRepository : ProjectionRepository, ISprintReadModelRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private static readonly string _tableName = "[dbo].[SprintReadModel]";

        public SprintReadModelRepository(ISqlConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory, _tableName)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<SprintReadModel>> GetSprintReadModelsAsync(string boardId)
        {
            var select = $"SELECT [BoardId], [SprintId], [Status], [Sequence] FROM {_tableName} WHERE [BoardId] = @BoardId";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            var args = new { BoardId = boardId };

            return await connection.QueryAsync<SprintReadModel>(select, args).ConfigureAwait(false);
        }

        public async Task SaveSprintReadModelAsync(SprintReadModel sprintReadModel)
        {
            var insert = $"INSERT INTO {_tableName} VALUES (@BoardId, @SprintId, @Status, @Sequence)";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            await connection.ExecuteAsync(insert, sprintReadModel).ConfigureAwait(false);
        }

        public async Task UpdateSprintReadModelStatusAsync(string boardId, string sprintId, string status)
        {
            var update = $"UPDATE {_tableName} SET [Status] = @Status WHERE [BoardId] = @BoardId AND [SprintId] = @SprintId";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            var args = new { BoardId = boardId, SprintId = sprintId, Status = status };

            await connection.ExecuteAsync(update, args).ConfigureAwait(false);
        }
    }
}
