using Dapper;
using Tacta.EventStore.Repository;
using Tactify.Core.ReadModels.BoardReadModels;
using Tactify.Core.ReadModels.BoardReadModels.Repositories;

namespace Tactify.Sql.Repositories
{
    public sealed class BoardReadModelRepository : ProjectionRepository, IBoardReadModelRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private static readonly string _tableName = "[dbo].[BoardReadModel]";

        public BoardReadModelRepository(ISqlConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory, _tableName)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task ArchiveBoardReadModelAsync(string boardId)
        {
            var update = $"UPDATE {_tableName} SET [IsArchived] = 1 WHERE [BoardId] = @BoardId";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            var args = new { BoardId = boardId };

            await connection.ExecuteAsync(update, args).ConfigureAwait(false);
        }

        public async Task<IEnumerable<BoardReadModel>> GetBoardReadModelsAsync()
        {
            var select = $"SELECT [BoardId], [Description], [IsArchived], [Sequence] FROM {_tableName}";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            return await connection.QueryAsync<BoardReadModel>(select).ConfigureAwait(false);
        }

        public async Task SaveBoardReadModelAsync(BoardReadModel boardReadModel)
        {
            var insert = $"INSERT INTO {_tableName} VALUES (@BoardId, @Description, @IsArchived, @Sequence)";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            await connection.ExecuteAsync(insert, boardReadModel).ConfigureAwait(false);
        }
    }
}
