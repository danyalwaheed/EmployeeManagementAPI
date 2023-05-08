using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDApi.Models
{
    public partial class TblUser
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Passwordexp { get; set; }
        public string Role { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
