using FluentValidation;
using MRI.API.Common;
using MRI.API.Entity;

namespace MRI.API.Services
{
    public class DepartmentValidator : AbstractValidator<Department>
    {
        public DepartmentValidator(string type)
        {
            if (type != MRIConstants.Insert)
                RuleFor(Department => Department.Id).NotNull().WithMessage("Department ID can not be null");

            RuleFor(Department => Department.Name).NotNull().WithMessage("Department Name can not be null");
            RuleFor(Department => Department.Head).NotNull().WithMessage("Department Head can not be null");
        }
    }
}
