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
using System.Threading.Tasks;

namespace MRI.API.EmployeeManagement.Controllers
{
    [ApiController]
    [Route(MRIConstants.ControllerBaseRoute)]
    public class DepartmentController : ControllerBase
    {
        #region Private variables
        private readonly IDepartmentService _departmentService;
        private readonly ILogger _logger;
        #endregion

        #region ctor
        /// <summary>
        /// DepartmentControler constructor
        /// </summary>
        /// <param name="departmentService">DepartmentService Interface</param>
        /// <param name="logger">Logger interface</param>
        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Get Department details
        /// </summary>
        /// <param name="Id">Department ID</param>
        /// <returns>Department details if valid id provided, else null response</returns>
        [HttpGet]
        [Route(MRIConstants.DepartmentRoute)]
        public async Task<ActionResult<List<Department>>> GetDepartment([FromQuery] string Id = "")
        {
            List<Department> response = new List<Department>();
            try
            {
                //if Department id is null or 0, than we need to fetch all the department details
                //decided business logic, we can tweak accordingly
                if (string.IsNullOrEmpty(Id) || Id.Trim().Equals(0))
                {
                    IEnumerable<Department> result = await _departmentService.GetAllDepartment();

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
                    Department result = await _departmentService.GetDepartmentDetails(Id);
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
                _logger.LogError("An error occurred in MRI.API.EmployeeManagement.Controllers.GetDepartment Method", ex);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, MRIConstants.ServiceUnavailable);
            }
        }
        /// <summary>
        /// Set Department details
        /// </summary>
        /// <param name="department">Department Entity</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MRIConstants.SetDepartmentRoute)]
        public async Task<ActionResult<CustomResponse>> SetDepartment(Department department)
        {
            try
            {
                #region Validation
                DepartmentValidator validator = new DepartmentValidator(MRIConstants.Insert);
                ValidationResult result = validator.Validate(department);
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
                    return await _departmentService.SetDepartmentDetails(department);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in MRI.API.EmployeeManagement.Controllers.SetDepartment Method", ex);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, MRIConstants.ServiceUnavailable);
            }
        }
        /// <summary>
        /// Update Employee details
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(MRIConstants.UpdateDepartmentRoute)]
        public async Task<ActionResult<CustomResponse>> UpdateDepartment(Department department)
        {
            try
            {
                DepartmentValidator validator = new DepartmentValidator(MRIConstants.Update);
                ValidationResult result = validator.Validate(department);

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
                    return await _departmentService.UpdateDepartmentDetails(department);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in MRI.API.EmployeeManagement.Controllers.UpdateDepartment Method", ex);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, MRIConstants.ServiceUnavailable);
            }
        }

        /// <summary>
        /// Remove Department
        /// </summary>
        /// <param name="Id">Department Id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MRIConstants.RemoveDepartmentRoute)]
        public async Task<ActionResult<CustomResponse>> RemoveDepartment([FromQuery] string Id)
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
                    return await _departmentService.RemoveDepartmentDetails(Convert.ToInt32(Id));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in MRI.API.EmployeeManagement.Controllers.RemoveDepartment Method", ex);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, MRIConstants.ServiceUnavailable);
            }
        }
    }
}
