using MRI.API.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MRI.API.Services
{
    public interface IDepartmentService
    {
        Task<Department> GetDepartmentDetails(string id);
        Task<IEnumerable<Department>> GetAllDepartment();

        Task<CustomResponse> SetDepartmentDetails(Department department);
        Task<CustomResponse> UpdateDepartmentDetails(Department department);
        Task<CustomResponse> RemoveDepartmentDetails(int Id);

    }
}
