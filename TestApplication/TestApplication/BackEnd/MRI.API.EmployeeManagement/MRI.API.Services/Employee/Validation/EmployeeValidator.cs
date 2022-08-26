using FluentValidation;
using MRI.API.Common;
using MRI.API.Entity;

namespace MRI.API.Services
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator(string type)
        {
            /*if(type != MRIConstants.Insert)
                RuleFor(Employee => Employee.Id).NotNull().WithMessage("Employee ID is required");

            RuleFor(Employee => Employee.DepartmentId).NotNull().WithMessage("Department Id is required");
            RuleFor(Employee => Employee.Email).NotNull().EmailAddress().WithMessage("Invalid Email address");
            RuleFor(Employee => Employee.Designation).NotNull().WithMessage("Designation is required");
            RuleFor(Employee => Employee.FirstName).NotNull().WithMessage("FirstName is required");*/
        }
    }
}
