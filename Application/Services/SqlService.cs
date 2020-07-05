using Microsoft.Data.SqlClient;
using System.Data;

namespace SampleMvcApp.Services
{
    public interface ISqlService
    {
        IDbConnection Connection { get; }
        SqlConnection SqlConnection { get; }
    }

    public class SqlService : ISqlService
    {
        private readonly string _connectionString;

        public SqlService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Connection => new SqlConnection(_connectionString);

        public SqlConnection SqlConnection => Connection as SqlConnection;
    }
}
