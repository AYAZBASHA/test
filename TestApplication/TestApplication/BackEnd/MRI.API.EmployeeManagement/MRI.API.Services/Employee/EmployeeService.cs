using Microsoft.Extensions.Logging;
using MRI.API.Data;
using MRI.API.Entity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MRI.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        #region Private Variables
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger _logger;
        #endregion

        #region ctor
        /// <summary>
        /// EmployeeService constructor
        /// </summary>
        /// <param name="employeeRepository">EmployeeRepository Interface</param>
        /// <param name="logger">Logger Interface</param>
        public EmployeeService(IEmployeeRepository employeeRepository, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Get all the employee details
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            try
            {
                IEnumerable<Employee> employees = await _employeeRepository.GetAll();
                return employees;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in GetAllEmployee", ex);
                return null;
            }
        }

        /// <summary>
        /// Get employee details based on incoming ID parameter
        /// </summary>
        /// <param name="ID">Employee id</param>
        /// <returns></returns>
        public async Task<Employee> GetEmployeeDetails(int Id)
        {
            try
            {
                Employee employee = await _employeeRepository.Get(Id);
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in GetAllEmployee", ex);
                return null;
            }
        }

        /// <summary>
        /// Set employee details
        /// </summary>
        /// <param name="employee">Employee entity</param>
        /// <returns></returns>
        public async Task<CustomResponse> SetEmployeeDetails(Employee employee)
        {
            CustomResponse response = new CustomResponse();
            Employee e = await _employeeRepository.Add(employee);
            if (e != null)
            {
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "Created Successfully - id :" + employee.Id;
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.NoContent;
                response.Message = "Employee is not created";
            }
            return response;
        }

        /// <summary>
        /// Update employee details
        /// </summary>
        /// <param name="employee">Employee entity</param>
        /// <returns></returns>
        public async Task<CustomResponse> UpdateEmployeeDetails(Employee employee)
        {
            CustomResponse response = new CustomResponse();
            Employee e = await _employeeRepository.Update(employee);
            if (e != null)
            {
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Updated Successfully - id :" + employee.Id;
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.NoContent;
                response.Message = "Not Updated for id -" +employee.Id;
            }
            return response;
        }

        /// <summary>
        /// Remove employee details
        /// </summary>
        /// <param name="employee">Employee entity</param>
        /// <returns></returns>
        public async Task<CustomResponse> RemoveEmployeeDetails(int Id)
        {
            CustomResponse response = new CustomResponse();
            Employee e = await _employeeRepository.Delete(Id);
            if (e != null)
            {
                response.StatusCode = (int)HttpStatusCode.Accepted;
                response.Message = "Deleted Successfully - id :" + Id;
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.NoContent;
                response.Message = "Not deleted for id "+ Id;
            }
            return response;
        }
    }
}
