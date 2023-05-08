using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.ViewModels.Request
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }

        public int CompanyId { get; set; }
        public int BranchId { get; set; }

    }
}
