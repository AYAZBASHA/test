using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MRI.API.Common;
using MRI.API.Entity;
using MRI.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MRI.API.EmployeeManagement.Controllers
{
    [ApiController]
    [Route(MRIConstants.ControllerBaseRoute)]
    public class EmployeeController : ControllerBase
    {
        #region Private variables
        private readonly IEmployeeService _employeeService;
        private readonly ILogger _logger = null;
        #endregion

        #region ctor
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Get Employee details
        /// </summary>
        /// <param name="Id">Employee ID</param>
        /// <returns>Employee details if valid id provided, else null response</returns>
        [HttpGet]
        [Route(MRIConstants.EmployeeRoute)]
        public async Task<ActionResult<List<Employee>>> GetEmployee([FromQuery] string Id = "")
        {         
            List<Employee> response = new List<Employee>();
            try
            {
                //if Employee id is null or 0, than we need to fetch all the employee details
                if (string.IsNullOrEmpty(Id) || Id.Trim().Equals(0))
                {
                    IEnumerable<Employee> result = await _employeeService.GetAllEmployee();
                    if (result != null || result.ToList().Count > 0)
                    {
                        return result.ToList();
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status204NoContent, MRIConstants.NoContentMessage);
                    }
                }
                else
                {
                    Employee result = await _employeeService.GetEmployeeDetails(Convert.ToInt32(Id));
                    if (result == null)
                    {
                        return StatusCode(StatusCodes.Status204NoContent, MRIConstants.NoContentMessage);
                    }
                    else
                    {
                        response.Add(result);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in MRI.API.EmployeeManagement.Controllers.GetEmployee Method", ex);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, MRIConstants.ServiceUnavailable);
            }
        }
        /// <summary>
        /// Set Employee details
        /// </summary>
        /// <param name="employee">Employee entity</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MRIConstants.SetEmployeeRoute)]
        public async Task<ActionResult<CustomResponse>> SetEmployee(Employee employee)
        {
            try
            {
                #region Validation
                EmployeeValidator validator = new EmployeeValidator(MRIConstants.Insert);
                ValidationResult result = validator.Validate(employee);
                #endregion

                if (!result.IsValid)
                {
                    foreach (var failures in result.Errors)
                    {
                        _logger.LogInformation($"Property [{failures.PropertyName}] failed validation. Error was [{failures.ErrorMessage}]");
                    }
                    CustomResponse response = new CustomResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = MRIConstants.InvalidParameter
                    };
                    return response;
                }
                else
                {
                    return await _employeeService.SetEmployeeDetails(employee);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in MRI.API.EmployeeManagement.Controllers.SetEmployee Method", ex);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, MRIConstants.ServiceUnavailable);
            }
        }
        /// <summary>
        /// Update Employee details
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(MRIConstants.UpdateEmployeeRoute)]
        public async Task<ActionResult<CustomResponse>> UpdateEmployee(Employee employee)
        {
            try
            {
                EmployeeValidator validator = new EmployeeValidator(MRIConstants.Update);
                ValidationResult result = validator.Validate(employee);

                if (!result.IsValid)
                {
                    foreach (var failures in result.Errors)
                    {
                        _logger.LogInformation($"Property [{failures.PropertyName}] failed validation. Error was [{failures.ErrorMessage}]");
                    }
                    CustomResponse response = new CustomResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = MRIConstants.InvalidParameter
                    };
                    return response;
                }
                else
                {
                    return await _employeeService.UpdateEmployeeDetails(employee);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in MRI.API.EmployeeManagement.Controllers.UpdateEmployee Method", ex);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, MRIConstants.ServiceUnavailable);
            }
        }

        /// <summary>
        /// Remove employee
        /// </summary>
        /// <param name="employee">Employee entity</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MRIConstants.RemoveEmployeeRoute)]
        public async Task<ActionResult<CustomResponse>> RemoveEmployee([FromQuery] string Id)
        {
            try
            {
                bool isValid = string.IsNullOrEmpty(Id) ? false : int.TryParse(Id, out _);

                if (!isValid)
                {
                    CustomResponse response = new CustomResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = MRIConstants.InvalidParameter
                    };
                    return response;
                }
                else
                {
                    return await _employeeService.RemoveEmployeeDetails(Convert.ToInt32(Id));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in MRI.API.EmployeeManagement.Controllers.RemoveEmployee Method", ex);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, MRIConstants.ServiceUnavailable);
            }
        }
    }
}
