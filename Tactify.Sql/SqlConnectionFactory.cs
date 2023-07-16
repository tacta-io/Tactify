using Microsoft.Data.SqlClient;
using System.Data;
using Tacta.EventStore.Repository;

namespace Tactify.Sql
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {

        private readonly string _connectionString;

        public IDbTransaction? Transaction => null;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public virtual string ConnectionString() => _connectionString;

        IDbConnection ISqlConnectionFactory.SqlConnection() => new SqlConnection(ConnectionString());
    }
}
