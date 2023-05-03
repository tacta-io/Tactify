using Dapper;
using Tacta.EventStore.Repository;
using Tactify.Core.ReadModels.ActivityReadModels;
using Tactify.Core.ReadModels.ActivityReadModels.Repositories;

namespace Tactify.Sql.Repositories.ReadModels
{
    public sealed class ActivityReadModelRepository : ProjectionRepository, IActivityReadModelRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private static readonly string _tableName = "[dbo].[ActivityReadModel]";

        public ActivityReadModelRepository(ISqlConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory, _tableName)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task InsertAsync(ActivityReadModel activityReadModel)
        {
            var insert = $"INSERT INTO {_tableName} VALUES (@CreatedAt, @CreatedBy, @Name, @Description, @Sequence)";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            await connection.ExecuteAsync(insert, activityReadModel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ActivityReadModel>> GetAsync()
        {
            var select = 
                @$"SELECT TOP (100) [CreatedAt], [CreatedBy], [Name], [Description], [Sequence] 
                FROM {_tableName} ORDER BY [Sequence] DESC";

            await using var connection = _sqlConnectionFactory.SqlConnection();

            return await connection.QueryAsync<ActivityReadModel>(select).ConfigureAwait(false);
        }
    }
}
