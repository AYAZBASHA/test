using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using static MRI.API.Common.MRIConstants;

namespace MRI.API.Data.Abstraction
{
    public interface IDBConnectionFactory
    {
        Task<IEnumerable<T>> ExecuteProcedure<T>(IDbConnection connection, string procName, CommandType commandType, DynamicParameters Parameters);
        Task<T> ExecuteProcedureSingle<T>(IDbConnection connection, string procName, CommandType commandType, DynamicParameters Parameters);

        Task<int> ExecuteStoreProcedure(IDbConnection connection, string procName, CommandType commandType, DynamicParameters parameters);

        Task<T> ExecuteScalarAsync<T>(IDbConnection connection, string procName, CommandType commandType, DynamicParameters parameters);
        DbConnection GetDbConnection(DatabaseConnectionName connectionName);
    }
}
