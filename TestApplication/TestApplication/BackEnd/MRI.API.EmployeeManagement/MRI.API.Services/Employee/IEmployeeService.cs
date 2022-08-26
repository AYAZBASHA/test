using MRI.API.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MRI.API.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployee();
        Task<Employee> GetEmployeeDetails(int Id);

        Task<CustomResponse> SetEmployeeDetails(Employee employee);
        Task<CustomResponse> UpdateEmployeeDetails(Employee employee);
        Task<CustomResponse> RemoveEmployeeDetails(int Id);

    }
}
