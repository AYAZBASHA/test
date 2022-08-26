using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace MRI.API.Entity
{
    /// <summary>
    /// Employee entity which stores basic information about all the employee
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the Employee GUID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  Gets or sets the Employee First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///  Gets or sets the Employee Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///  Gets or sets the Employee Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Employee Designation
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or sets the Employee Departmentid
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets the Employee Departmentid
        /// </summary>
        public string DepartmentName { get; set; }
    }
}
