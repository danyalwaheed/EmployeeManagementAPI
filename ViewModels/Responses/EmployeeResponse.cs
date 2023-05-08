using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.ViewModels.Responses
{
    public class EmployeeResponse : DepartmentResponse
    {
        public string EmployeeName { get; set; }
        public int EmployeeID { get; set; }
        public string BranchName { get; set; }
        public string EmployeeEmail { get; set; }
        public string PhotoPath { get; set; }
        public string CompanyName { get; set; }
        public string CountryName { get; set; }
        public string CompanyAddress { get; set; }
        public string Nationality { get; set; }
        public string Status { get; set; }
    }
}