using System;

namespace MRI.API.Entity
{
    /// <summary>
    /// Department entity stores basic information about department
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Gets or sets the Department GUID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Department Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Department Head
        /// </summary>
        public string Head { get; set; }

        /// <summary>
        /// Gets or sets the Location
        /// </summary>
        public string Location { get; set; }
    }
}
