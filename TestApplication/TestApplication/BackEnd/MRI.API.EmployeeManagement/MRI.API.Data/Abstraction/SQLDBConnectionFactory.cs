using Dapper;
using MRI.API.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace MRI.API.Data.Abstraction
{
    public class SQLDBConnectionFactory : IDBConnectionFactory
    {
        public Task<IEnumerable<T>> ExecuteProcedure<T>(IDbConnection connection, string procName, CommandType commandType, DynamicParameters Parameters)
        {
            throw new NotImplementedException();
        }

        public Task<T> ExecuteProcedureSingle<T>(IDbConnection connection, string procName, CommandType commandType, DynamicParameters Parameters)
        {
            throw new NotImplementedException();
        }

        public Task<T> ExecuteScalarAsync<T>(IDbConnection connection, string procName, CommandType commandType, DynamicParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteStoreProcedure(IDbConnection connection, string procName, CommandType commandType, DynamicParameters parameters)
        {
            throw new NotImplementedException();
        }

        public DbConnection GetDbConnection(MRIConstants.DatabaseConnectionName connectionName)
        {
            throw new NotImplementedException();
        }
    }
}
