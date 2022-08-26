using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using static MRI.API.Common.MRIConstants;

namespace MRI.API.Data.Abstraction
{
    public class DBConnectionFactory : IDBConnectionFactory
    {
        #region Private Variables
        private DbConnection _dbConnection;
        private readonly string _connectionString = string.Empty;
        #endregion

        #region ctor
        public DBConnectionFactory(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new NullReferenceException("Connection string value is not set");

            _connectionString = connectionString;
        }
        #endregion

        #region Methods for getting a database connection
        /// <summary>
        /// Method to get a database connection based on the connectionname we pass as a parameter
        /// </summary>
        /// <param name="connectionName">connectionName</param>
        /// <returns>Null if connection name is pass other than SQL,MYSQL or Postgre, throws exception if error occur, returns valid database connection</returns>
        public DbConnection GetDbConnection(DatabaseConnectionName connectionName)
        {
            try
            {
                switch (connectionName)
                {
                    case DatabaseConnectionName.SQLConnection:
                        {
                            return GetSQLConnection(_connectionString);
                        }
                    case DatabaseConnectionName.MYSQLConnection:
                        {
                            return GetMySQLConnection(_connectionString);
                        }
                    case DatabaseConnectionName.PostGreSQLConnection:
                        {
                            return GetPostGreSQLConnection(_connectionString);
                        }
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get SQL Connection
        /// </summary>
        /// <param name="connectionString">SQL connection string</param>
        /// <returns>DbConnection object , throws exception if any error occurrs</returns>
        private DbConnection GetSQLConnection(string connectionString)
        {
            try
            {
                object _lock = new object();
                // Create a new connection if null or disposed
                if (_dbConnection == null)
                {
                    // Lock is used to control the no of connections which is opened by application
                    // At a same time if 1000 request came than we no need create 1000 open connections
                    // This implementation is called double check locking pattern
                    lock (_lock)
                    {
                        if (_dbConnection == null)
                        {
                            _dbConnection = new SqlConnection(connectionString);
                            _dbConnection.Open();
                        }
                        // Open the connection is connection state is close or dispose
                        else if (_dbConnection.State != System.Data.ConnectionState.Open)
                        {
                            _dbConnection.Open();
                        }
                    }
                }

                return _dbConnection;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private DbConnection GetMySQLConnection(string connectionString)
        {
            return null;
        }
        private DbConnection GetPostGreSQLConnection(string connectionString)
        {
            return null;
        }
        #endregion // End - Methods for getting a database connection
        private void OpenConnection(IDbConnection connection)
        {
            try
            {
                if (connection == null)
                    throw new NullReferenceException("Connection is closed");

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Generic implementation for executing stored procedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">DB Connection</param>
        /// <param name="procName">procudure name</param>
        /// <param name="commandType">command type</param>
        /// <param name="Parameters">parameters of stored procedure</param>
        /// <returns>List with values, throws an exception if error occurrs</returns>
        public async Task<IEnumerable<T>> ExecuteProcedure<T>(IDbConnection connection, string procName, CommandType commandType, DynamicParameters Parameters)
        {
            IDbTransaction transaction = null;
            try
            {
                OpenConnection(connection);
                transaction = connection.BeginTransaction();
                var result = await connection.QueryAsync<T>(procName, Parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                transaction.Commit();
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// Generic implementation for executing stored procedure
        /// </summary>
        /// <typeparam name="T">Any type of entity</typeparam>
        /// <param name="connection">DB Connection</param>
        /// <param name="procName">procudure name</param>
        /// <param name="commandType">command type</param>
        /// <param name="Parameters">parameters of stored procedure</param>
        /// <returns>Single of default value</returns>
        public async Task<T> ExecuteProcedureSingle<T>(IDbConnection connection, string procName, CommandType commandType, DynamicParameters parameters)
        {
            IDbTransaction transaction = null;
            try
            {

                OpenConnection(connection);

                transaction = connection.BeginTransaction();

                var result = await connection.QuerySingleOrDefaultAsync<T>(procName, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
              
                transaction.Commit();

                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Execute procedure which returns number of rows addected
        /// </summary>
        /// <param name="connection">Databsae connection</param>
        /// <param name="procName">Procedure Name</param>
        /// <param name="commandType">Command Type</param>
        /// <param name="parameters">StoredProcedure Parameter</param>
        /// <returns></returns>
        public async Task<int> ExecuteStoreProcedure(IDbConnection connection,string procName,CommandType commandType,DynamicParameters parameters)
        {
            IDbTransaction transaction = null;
            try
            {
                OpenConnection(connection);

                transaction = connection.BeginTransaction();

                int result = await connection.ExecuteAsync(procName, parameters, commandType: CommandType.StoredProcedure, transaction: transaction).ConfigureAwait(false);

                transaction.Commit();
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Execute and get a single value
        /// </summary>
        /// <typeparam name="T">Any type of entity</typeparam>
        /// <param name="connection">Database Connection</param>
        /// <param name="procName">Procedure Name</param>
        /// <param name="commandType">Command Type</param>
        /// <param name="parameters">StoredProcedure Parameter</param>
        /// <returns></returns>
        public async Task<T> ExecuteScalarAsync<T>(IDbConnection connection, string procName, CommandType commandType, DynamicParameters parameters)
        {
            IDbTransaction transaction = null;
            try
            {
                OpenConnection(connection);

                transaction = connection.BeginTransaction();

                T result = await connection.ExecuteScalarAsync<T>(procName, parameters, commandType: CommandType.StoredProcedure, transaction: transaction).ConfigureAwait(false);

                transaction.Commit();
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
