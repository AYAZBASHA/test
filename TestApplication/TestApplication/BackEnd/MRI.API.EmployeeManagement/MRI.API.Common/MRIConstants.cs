using System;

namespace MRI.API.Common
{
    public static class MRIConstants
    {

        #region Routing related constants
        public const string ControllerBaseRoute = "api/v1/[controller]";
        public const string EmployeeRoute = "GetEmployee";
        public const string DepartmentRoute = "GetDepartment";
        public const string SetEmployeeRoute = "SetEmployee";
        public const string SetDepartmentRoute = "SetDepartment";
        public const string UpdateEmployeeRoute = "UpdateEmployee";
        public const string UpdateDepartmentRoute = "UpdateDepartment";
        public const string RemoveEmployeeRoute = "RemoveEmployee";
        public const string RemoveDepartmentRoute = "RemoveDepartment";
        #endregion

        #region Custom Messages
        public const string ServiceUnavailable = "Service temperory unavailable. Please try after some time";
        public const string NoContentMessage = "Requested data is not available";
        public const string InvalidParameter = "Invalid parameters are supplied";
        #endregion

        #region Stored Procedure 
        public const string SP_GetDepartmentDetails = "GetDepartmentDetails";
        public const string SP_GetEmployeeDetails = "GetEmployeeDetails";
        public const string SP_SetDepartmentDetails = "SetDepartmentDetails";
        public const string SP_SetEmployeeDetails = "SetEmployeeDetails";
        public const string SP_RemoveDepartmentDetails = "RemoveDepartmentDetails";
        public const string SP_RemoveEmployeeDetails = "RemoveEmployeeDetails";
        #endregion

        #region Store Procedure Parameters
        public const string SP_Param_ID = "Id";

        //Employee related stored procedure parameter
        public const string SP_Param_FirstName = "FirstName";
        public const string SP_Param_LastName = "LastName";
        public const string SP_Param_Email = "Email";
        public const string SP_Param_Designation = "Designation";
        public const string SP_Param_Department = "Department";
        public const string SP_Param_Operation = "Operation";

        //Department related stored procedure parameter
        public const string SP_Param_Dep_Name = "Name";
        public const string SP_Param_Dep_Location = "Location";
        public const string SP_Param_Dep_Head = "Head";

        public const string Insert = "Insert";
        public const string Update = "Update";

        #endregion

        #region DatabaseEnums
        public enum DatabaseConnectionName
        {
            SQLConnection,
            PostGreSQLConnection,
            MYSQLConnection
        }
        #endregion
    }
}   
