using Dapper;
using Tacta.Connection;
using Tacta.EventStore.Repository;
using Tactify.Core.ReadModels.BoardReadModels;
using Tactify.Core.ReadModels.BoardReadModels.Repositories;

namespace Tactify.Sql.Repositories.ReadModels
{
    public sealed class BoardReadModelRepository : ProjectionRepository, IBoardReadModelRepository
    {
        private readonly IConnectionFactory _sqlConnectionFactory;

        private static readonly string _tableName = "[dbo].[BoardReadModel]";

        public BoardReadModelRepository(IConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory, _tableName)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<BoardReadModel>> GetAsync()
        {
            var select = $"SELECT [BoardId], [Description], [IsArchived], [Sequence] FROM {_tableName}";

            await using var connection = _sqlConnectionFactory.Connection();

            return await connection.QueryAsync<BoardReadModel>(select).ConfigureAwait(false);
        }

        public async Task OnBoardCreatedAsync(BoardReadModel boardReadModel)
        {
            var insert = $"INSERT INTO {_tableName} VALUES (@BoardId, @Description, @IsArchived, @Sequence)";

            await using var connection = _sqlConnectionFactory.Connection();

            await connection.ExecuteAsync(insert, boardReadModel).ConfigureAwait(false);
        }

        public async Task OnBoardArchivedAsync(BoardReadModel boardReadModel)
        {
            var update = $"UPDATE {_tableName} SET [IsArchived] = @IsArchived WHERE [BoardId] = @BoardId";

            await using var connection = _sqlConnectionFactory.Connection();

            await connection.ExecuteAsync(update, boardReadModel).ConfigureAwait(false);
        }      
    }
}
