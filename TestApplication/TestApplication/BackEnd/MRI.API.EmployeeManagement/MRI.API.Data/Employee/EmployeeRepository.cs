using Dapper;
using MRI.API.Common;
using MRI.API.Data.Abstraction;
using MRI.API.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MRI.API.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        #region Private Variables
        private readonly IDBConnectionFactory _dbConnectionFactory;
        private readonly IDbConnection _dbConnection;
        #endregion

        #region ctor
        public EmployeeRepository(IDBConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _dbConnection = dbConnectionFactory.GetDbConnection(MRIConstants.DatabaseConnectionName.SQLConnection);
        }
        #endregion


        /// <summary>
        /// Get Single employee details
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Employee details based on id, throws an exception if an error occurrs</returns>
        public async Task<Employee> Get(object id)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add(MRIConstants.SP_Param_ID, id, DbType.Int32);
                Employee employee = await _dbConnectionFactory.ExecuteProcedureSingle<Employee>(_dbConnection, MRIConstants.SP_GetEmployeeDetails, CommandType.StoredProcedure, parameter);
                return employee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get All employee details
        /// </summary>
        /// <returns>All employee details,null or throws exception is any error occurrs</returns>
        public async Task<IEnumerable<Employee>> GetAll()
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add(MRIConstants.SP_Param_ID, 0, System.Data.DbType.Int32);
                IEnumerable<Employee> employees = await _dbConnectionFactory.ExecuteProcedure<Employee>(_dbConnection, MRIConstants.SP_GetEmployeeDetails, CommandType.StoredProcedure, parameter);
                return employees;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Add Employee
        /// </summary>
        /// <param name="entity">Employee Entity</param>
        /// <returns></returns>
        public async Task<Employee> Add(Employee entity)
        {
            try
            {
                bool res = await SetEmployeeDetails(entity, MRIConstants.Insert);
                if (!res)
                    return null;
                else
                    return new Employee();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="entity">Employee Entity</param>
        /// <returns></returns>
        public async Task<Employee> Update(Employee entity)
        {
            try
            {
                bool res = await SetEmployeeDetails(entity, MRIConstants.Update);
                if (!res)
                    return null;
                else
                    return new Employee();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Set employee details
        /// </summary>
        /// <param name="employee">Employee Entity</param>
        /// <param name="operation">Inser or update</param>
        /// <returns>true if inserted, false if not inserted , throws exception is any error occurred</returns>
        private async Task<bool> SetEmployeeDetails(Employee employee, string operation)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add(MRIConstants.SP_Param_ID, employee.Id, DbType.Int32);
                parameter.Add(MRIConstants.SP_Param_FirstName, employee.FirstName, DbType.String);
                parameter.Add(MRIConstants.SP_Param_LastName, employee.LastName, DbType.String);
                parameter.Add(MRIConstants.SP_Param_Email, employee.Email, DbType.String);
                parameter.Add(MRIConstants.SP_Param_Designation, employee.Designation, DbType.String);
                parameter.Add(MRIConstants.SP_Param_Department, employee.DepartmentId, DbType.Int32);
                parameter.Add(MRIConstants.SP_Param_Operation, operation, DbType.String);
                int res = await _dbConnectionFactory.ExecuteStoreProcedure(_dbConnection, MRIConstants.SP_SetEmployeeDetails, CommandType.StoredProcedure, parameter);
                if (res == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Remove Employee
        /// </summary>
        /// <param name="Id">Employee Id</param>
        /// <returns></returns>
        public async Task<Employee> Delete(object Id)
        {
            try
            {
                bool res = await RemoveEmployeeDetails(Convert.ToInt32(Id));
                if (!res)
                    return null;
                else
                    return new Employee();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Remove employee
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>true if deleted, false if not , throws exception is any error occurred</returns>
        private async Task<bool> RemoveEmployeeDetails(int id)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add(MRIConstants.SP_Param_ID, id, DbType.Int32);
                int res = await _dbConnectionFactory.ExecuteStoreProcedure(_dbConnection, MRIConstants.SP_RemoveEmployeeDetails, CommandType.StoredProcedure, parameter);
                if (res == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
