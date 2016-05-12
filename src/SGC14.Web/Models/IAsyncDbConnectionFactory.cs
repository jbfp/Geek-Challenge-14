using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SGC14.Web.Models
{
    public interface IAsyncDbConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync();
        Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken);
    }
}