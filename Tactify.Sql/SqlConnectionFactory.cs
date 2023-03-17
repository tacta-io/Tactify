using Microsoft.Data.SqlClient;
using Tacta.EventStore.Repository;

namespace Tactify.Sql
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {

        private readonly string _connectionString;       

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection SqlConnection() => new SqlConnection(ConnectionString());

        public virtual string ConnectionString() => _connectionString;
    }
}
