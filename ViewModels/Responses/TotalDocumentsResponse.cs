using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.ViewModels.Responses
{
    public class TotalDocumentsResponse

    {
        public int EmployeeId { get; set; }
        //  public int DepartmentId { get; set; }
        public int? DocumentId { get; set; }
        public string EmployeeName { get; set; }
        public string DocumentName { get; set; }
        public int DocumentCount { get; set; }
    }
}