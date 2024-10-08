﻿using Dapper;
using Tacta.Connection;
using Tacta.EventStore.Repository;
using Tactify.Core.ReadModels.TicketReadModels;
using Tactify.Core.ReadModels.TicketReadModels.Repositories;

namespace Tactify.Sql.Repositories.ReadModels
{
    public sealed class TicketReadModelRepository : ProjectionRepository, ITicketReadModelRepository
    {
        private readonly IConnectionFactory _sqlConnectionFactory;

        private static readonly string _tableName = "[dbo].[TicketReadModel]";

        public TicketReadModelRepository(IConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory, _tableName)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<TicketReadModel> GetAsync(string ticketId)
        {
            var select =
                @$"SELECT [TicketId], [BoardId], [SprintId], [Description], [Assignee], [Estimation], [IsClosed], [Sequence] 
                FROM {_tableName} 
                WHERE [TicketId] = @TicketId";

            await using var connection = _sqlConnectionFactory.Connection();

            var args = new { TicketId = ticketId };

            return await connection.QuerySingleOrDefaultAsync<TicketReadModel>(select, args).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TicketReadModel>> GetAsync(string boardId, string sprintId)
        {
            var select =
                @$"SELECT [TicketId], [BoardId], [SprintId], [Description], [Assignee], [Estimation], [IsClosed], [Sequence] 
                FROM {_tableName} 
                WHERE [BoardId] = @BoardId AND [SprintId] = @SprintId";

            await using var connection = _sqlConnectionFactory.Connection();

            var args = new { BoardId = boardId, SprintId = sprintId };

            return await connection.QueryAsync<TicketReadModel>(select, args).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TicketReadModel>> GetBacklogAsync(string boardId)
        {
            var select =
                @$"SELECT [TicketId], [BoardId], [SprintId], [Description], [Assignee], [Estimation], [IsClosed], [Sequence] 
                FROM {_tableName} 
                WHERE [BoardId] = @BoardId AND [SprintId] IS NULL";

            await using var connection = _sqlConnectionFactory.Connection();

            var args = new { BoardId = boardId };

            return await connection.QueryAsync<TicketReadModel>(select, args).ConfigureAwait(false);
        }

        public async Task OnTicketOpenedAsync(TicketReadModel ticketReadModel)
        {
            var insert =
                @$"INSERT INTO {_tableName} 
                VALUES (@TicketId, @BoardId, @SprintId, @Description, @Assignee, @Estimation, @IsClosed, @Sequence)";

            await using var connection = _sqlConnectionFactory.Connection();

            await connection.ExecuteAsync(insert, ticketReadModel).ConfigureAwait(false);
        }

        public async Task OnTicketEstimatedAsync(TicketReadModel ticketReadModel)
        {
            var update =
                @$"UPDATE {_tableName} 
                SET [Estimation] = @Estimation, [Sequence] = @Sequence
                WHERE [TicketId] = @TicketId";

            await using var connection = _sqlConnectionFactory.Connection();

            await connection.ExecuteAsync(update, ticketReadModel).ConfigureAwait(false);
        }

        public async Task OnTicketMovedToSprintAsync(TicketReadModel ticketReadModel)
        {
            var update =
                @$"UPDATE {_tableName} 
                SET [SprintId] = @SprintId, [Sequence] = @Sequence
                WHERE [TicketId] = @TicketId";

            await using var connection = _sqlConnectionFactory.Connection();

            await connection.ExecuteAsync(update, ticketReadModel).ConfigureAwait(false);
        }

        public async Task OnTicketAssignedAsync(TicketReadModel ticketReadModel)
        {
            var update =
                @$"UPDATE {_tableName} 
                SET [Assignee] = @Assignee, [Sequence] = @Sequence
                WHERE [TicketId] = @TicketId";

            await using var connection = _sqlConnectionFactory.Connection();

            await connection.ExecuteAsync(update, ticketReadModel).ConfigureAwait(false);
        }

        public async Task OnTicketClosedAsync(TicketReadModel ticketReadModel)
        {
            var update =
                @$"UPDATE {_tableName} 
                SET [IsClosed] = @IsClosed, [Sequence] = @Sequence
                WHERE [TicketId] = @TicketId";

            await using var connection = _sqlConnectionFactory.Connection();

            await connection.ExecuteAsync(update, ticketReadModel).ConfigureAwait(false);
        }
    }
}
