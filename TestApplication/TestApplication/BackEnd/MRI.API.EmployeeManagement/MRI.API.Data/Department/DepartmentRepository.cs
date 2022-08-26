using Dapper;
using MRI.API.Data.Abstraction;
using MRI.API.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using MRI.API.Common;
using System.Data;

namespace MRI.API.Data
{
    public class DepartmentRepository : IGenericRepository<Department>, IDepartmentRepository
    {
        #region Private variables
        private readonly IDBConnectionFactory _dbConnectionFactory;
        private readonly DbConnection _dbConnection;
        #endregion

        #region ctor
        /// <summary>
        /// DepartmentRepositoty Constructor
        /// </summary>
        /// <param name="dbConnectionFactory">DBConnection Factory Interface</param>
        public DepartmentRepository(IDBConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _dbConnection = _dbConnectionFactory.GetDbConnection(MRIConstants.DatabaseConnectionName.SQLConnection);
        }
        #endregion

        /// <summary>
        /// Add Department
        /// </summary>
        /// <param name="entity">Department Entity</param>
        /// <returns></returns>
        public async Task<Department> Add(Department entity)
        {
            try
            {
                bool res = await SetDepartmentDetails(entity, MRIConstants.Insert);
                if (!res)
                    return null;
                else
                    return new Department();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update department details
        /// </summary>
        /// <param name="entity">Department Details</param>
        /// <returns></returns>
        public async Task<Department> Update(Department entity)
        {
            try
            {
                bool res = await SetDepartmentDetails(entity, MRIConstants.Update);
                if (!res)
                    return null;
                else
                    return new Department();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Set Department details
        /// </summary>
        /// <param name="Department">Department Entity</param>
        /// <param name="operation">Inser or update</param>
        /// <returns>true if inserted, false if not inserted , throws exception is any error occurred</returns>
        private async Task<bool> SetDepartmentDetails(Department department, string operation)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add(MRIConstants.SP_Param_ID, department.Id, DbType.Int32);
                parameter.Add(MRIConstants.SP_Param_Dep_Name, department.Name, DbType.String);
                parameter.Add(MRIConstants.SP_Param_Dep_Location, department.Location, DbType.String);
                parameter.Add(MRIConstants.SP_Param_Dep_Head, department.Head, DbType.String);
                parameter.Add(MRIConstants.SP_Param_Operation, operation, DbType.String);
                int res = await _dbConnectionFactory.ExecuteStoreProcedure(_dbConnection, MRIConstants.SP_SetDepartmentDetails, CommandType.StoredProcedure, parameter);
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
        /// Remove Department
        /// </summary>
        /// <param name="Id">Department ID</param>
        /// <returns></returns>
        public async Task<Department> Delete(object Id)
        {
            try
            {
                bool res = await RemoveDepartmentDetails(Id);
                if (!res)
                    return null;
                else
                    return new Department();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Remove Department
        /// </summary>
        /// <param name="id">Department ID</param>
        /// <returns>true if deleted, false if not , throws exception is any error occurred</returns>
        private async Task<bool> RemoveDepartmentDetails(object id)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add(MRIConstants.SP_Param_ID, id, DbType.Int32);
                int res = await _dbConnectionFactory.ExecuteStoreProcedure(_dbConnection, MRIConstants.SP_RemoveDepartmentDetails, CommandType.StoredProcedure, parameter);
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
        /// Get Single department details
        /// </summary>
        /// <param name="id">Department Id</param>
        /// <returns>Department details based on id, throws an exception if an error occurrs</returns>
        public async Task<Department> Get(object id)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add(MRIConstants.SP_Param_ID, id, System.Data.DbType.Int32);
                Department department = await _dbConnectionFactory.ExecuteProcedureSingle<Department>(_dbConnection, MRIConstants.SP_GetDepartmentDetails, System.Data.CommandType.StoredProcedure, parameter);
                return department;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all the department details
        /// </summary>
        /// <returns>Get all the details related to department, throws and exception if an error occurrs</returns>
        public async Task<IEnumerable<Department>> GetAll()
        {   
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add(MRIConstants.SP_Param_ID, 0, System.Data.DbType.Int32);
                IEnumerable<Department> departments = await _dbConnectionFactory.ExecuteProcedure<Department>(_dbConnection, MRIConstants.SP_GetDepartmentDetails, System.Data.CommandType.StoredProcedure, parameter);
                return departments;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
