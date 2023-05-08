using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDApi.Models
{
    public partial class TblEmployeeDocument
    {
        [Key]
        public int EmployeeDocumentId { get; set; }
        public string DocumentPath { get; set; }
        public string Remarks { get; set; }
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public int DocumentCategoryId { get; set; }
    }
}
