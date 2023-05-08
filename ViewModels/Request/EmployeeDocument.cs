using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.ViewModels.Request
{
    public class EmployeeDocument
    {
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int DocumentCategoryId { get; set; }
        [Required]
        public IFormFile Document { get; set; }
        public string Remarks { get; set; }
    }
}
