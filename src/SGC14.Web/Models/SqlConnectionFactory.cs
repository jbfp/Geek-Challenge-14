using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace SGC14.Web.Models
{
    public class SqlConnectionFactory : IAsyncDbConnectionFactory
    {
        private readonly string connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }

            this.connectionString = connectionString;
        }

        public Task<IDbConnection> CreateConnectionAsync()
        {
            return CreateConnectionAsync(CancellationToken.None);
        }

        public async Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken)
        {
            var connection = new SqlConnection(this.connectionString);
            await connection.OpenAsync(cancellationToken);
            return connection;
        }
    }
}