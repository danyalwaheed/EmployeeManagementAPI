using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDApi.Models
{
    public partial class TblBranch
    {

        [Key]
        public  int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public int CompanyId { get; set; }
        public int CountryId { get; set; }
    }
}
