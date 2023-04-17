﻿using Dapper;
using Tacta.EventStore.Repository;
using Tactify.Core.ReadModels.ActivityReadModels;
using Tactify.Core.ReadModels.ActivityReadModels.Repositories;

namespace Tactify.Sql.Repositories
{
    public sealed class ActivityReadModelRepository : ProjectionRepository, IActivityReadModelRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private static readonly string _tableName = "[dbo].[ActivityReadModel]";

        public ActivityReadModelRepository(ISqlConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory, _tableName)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task SaveActivityReadModelAsync(ActivityReadModel activityReadModel)
        {
            var insert = $"INSERT INTO {_tableName} VALUES (@CreatedAt, @CreatedBy, @Name, @Description, @Sequence)";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            await connection.ExecuteAsync(insert, activityReadModel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ActivityReadModel>> GetActivityReadModelsAsync()
        {
            var select = $"SELECT [CreatedAt], [CreatedBy], [Name], [Description], [Sequence] FROM {_tableName}";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            return await connection.QueryAsync<ActivityReadModel>(select).ConfigureAwait(false);
        }
    }
}
