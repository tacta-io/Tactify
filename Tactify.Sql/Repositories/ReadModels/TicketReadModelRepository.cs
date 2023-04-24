using Dapper;
using Tacta.EventStore.Repository;
using Tactify.Core.ReadModels.TicketReadModels;
using Tactify.Core.ReadModels.TicketReadModels.Repositories;

namespace Tactify.Sql.Repositories.ReadModels
{
    public sealed class TicketReadModelRepository : ProjectionRepository, ITicketReadModelRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private static readonly string _tableName = "[dbo].[TicketReadModel]";

        public TicketReadModelRepository(ISqlConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory, _tableName)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<TicketReadModel>> GetAsync(string boardId, string? sprintId)
        {
            var select =
                @$"SELECT [TicketId], [BoardId], [SprintId], [Description], [Assignee], [Estimation], [IsClosed], [Sequence] 
                FROM {_tableName} 
                WHERE [BoardId] = @BoardId AND [SprintId] = @SprintId";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            var args = new { BoardId = boardId, SprintId = sprintId };

            return await connection.QueryAsync<TicketReadModel>(select, args).ConfigureAwait(false);
        }

        public async Task OnTicketOpenedAsync(TicketReadModel ticketReadModel)
        {
            var insert =
                @$"INSERT INTO {_tableName} 
                VALUES (@TicketId, @BoardId, @SprintId, @Description, @Assignee, @Estimation, @IsClosed, @Sequence)";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            await connection.ExecuteAsync(insert, ticketReadModel).ConfigureAwait(false);
        }

        public async Task OnTicketEstimatedAsync(TicketReadModel ticketReadModel)
        {
            var update =
                @$"UPDATE {_tableName} 
                SET [Estimation] = @Estimation, [Sequence] = @Sequence
                WHERE [TicketId] = @TicketId";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            await connection.ExecuteAsync(update, ticketReadModel).ConfigureAwait(false);
        }

        public async Task OnTicketMovedToSprintAsync(TicketReadModel ticketReadModel)
        {
            var update =
                @$"UPDATE {_tableName} 
                SET [SprintId] = @SprintId, [Sequence] = @Sequence
                WHERE [TicketId] = @TicketId";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            await connection.ExecuteAsync(update, ticketReadModel).ConfigureAwait(false);
        }

        public async Task OnTicketAssignedAsync(TicketReadModel ticketReadModel)
        {
            var update =
                @$"UPDATE {_tableName} 
                SET [Assignee] = @Assignee, [Sequence] = @Sequence
                WHERE [TicketId] = @TicketId";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            await connection.ExecuteAsync(update, ticketReadModel).ConfigureAwait(false);
        }

        public async Task OnTicketClosedAsync(TicketReadModel ticketReadModel)
        {
            var update =
                @$"UPDATE {_tableName} 
                SET [IsClosed] = @IsClosed, [Sequence] = @Sequence
                WHERE [TicketId] = @TicketId";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            await connection.ExecuteAsync(update, ticketReadModel).ConfigureAwait(false);
        }
    }
}
