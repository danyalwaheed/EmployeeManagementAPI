using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.ViewModels.Request
{
    public class EmployeeCreateVM
    {
        [Required]
        [MaxLength(30)]
        public string EmployeeName { get; set; }
        [Required]
        public string EmployeeEmail { get; set; }
        [Required]
        public int GrossSalary { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public int NationalityId { get; set; }
        [Required]
        public string PhotoPath { get; set; }
        [Required]
        public int BranchId { get; set; }
        // public IFormFile Photo { get; set; }
    }

    public class EmployeeUpdateVM : EmployeeCreateVM
    {
        [Required]
        public int EmployeeId { get; set; }
    }
}
