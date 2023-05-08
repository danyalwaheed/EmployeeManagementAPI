using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.ViewModels.Response
{
    public class BranchResponse
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public int CompanyId { get; set; }
        public int? CountryId { get; set; }
    }
}
