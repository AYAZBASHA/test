using Microsoft.Extensions.Logging;
using MRI.API.Data;
using MRI.API.Entity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MRI.API.Services
{
    public class DepartmentService : IDepartmentService
    {
        #region Private variables
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILogger _logger;
        #endregion

        #region ctor
        /// <summary>
        /// DepartmentService Constructor
        /// </summary>
        /// <param name="departmentRepository">departmentRepository Interface</param>
        /// <param name="logger">Logger interface</param>
        public DepartmentService(IDepartmentRepository departmentRepository, ILogger<DepartmentService> logger)
        {
            _departmentRepository = departmentRepository;
            _logger = logger;
        }
        #endregion
        /// <summary>
        /// Method to get all the details of department
        /// </summary>
        /// <returns>all department, returns null if any error occurred</returns>
        public async Task<IEnumerable<Department>> GetAllDepartment()
        {
            try
            {
                IEnumerable<Department> departments = null;
                departments = await _departmentRepository.GetAll();
                return departments;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in GetAllDepartment", ex);
                return null;
            }
        }

        /// <summary>
        /// Get department specific details
        /// </summary>
        /// <param name="id">department id</param>
        /// <returns>Department specific details based on id, returns null if error or exception occurred</returns>
        public async Task<Department> GetDepartmentDetails(string id)
        {
            try
            {
                Department department;
                department = await _departmentRepository.Get(id);
                return department;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in GetDepartmentDetails", ex);
                return null;
            }
        }

        /// <summary>
        /// Set Department Details
        /// </summary>
        /// <param name="department">Department Entity</param>
        /// <returns></returns>
        public async Task<CustomResponse> SetDepartmentDetails(Department department)
        {
            CustomResponse response = new CustomResponse();
            Department e = await _departmentRepository.Add(department);
            if (e != null)
            {
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "Created Successfully - id :" + department.Id;
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.NoContent;
                response.Message = "Department is not created";
            }
            return response;
        }

        /// <summary>
        /// Update Department details
        /// </summary>
        /// <param name="department">Department entity</param>
        /// <returns></returns>
        public async Task<CustomResponse> UpdateDepartmentDetails(Department department)
        {
            CustomResponse response = new CustomResponse();
            Department d = await _departmentRepository.Update(department);
            if (d != null)
            {
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Updated Successfully - id :" + department.Id;
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.NoContent;
                response.Message = "Not Updated for id -" + department.Id;
            }
            return response;
        }

        /// <summary>
        /// Remove Department Details
        /// </summary>
        /// <param name="Id">Department ID</param>
        /// <returns></returns>
        public async Task<CustomResponse> RemoveDepartmentDetails(int Id)
        {
            CustomResponse response = new CustomResponse();
            Department d =   await _departmentRepository.Delete(Id);
            if (d != null)
            {
                response.StatusCode = (int)HttpStatusCode.Accepted;
                response.Message = "Deleted Successfully - id :" + Id;
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.NoContent;
                response.Message = "Not deleted for id " + Id;
            }
            return response;
        }
    }
}
