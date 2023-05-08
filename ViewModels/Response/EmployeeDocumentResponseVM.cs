using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.ViewModels.Response
{
    public class EmployeeDocumentResponseVM
    {
        public int EmployeeDocumentID { get; set; }
        public string DepartmentName { get; set; }
        public string DocumentTypeName { get; set; }
        public string DocumentName { get; set; }
        public string Remarks { get; set; }
    }
}
