using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRUDApi.Models
{
    public partial class EmployeeApiDBContext : DbContext
    {
        public EmployeeApiDBContext()
        {
        }

        public EmployeeApiDBContext(DbContextOptions<EmployeeApiDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblBranch> TblBranch { get; set; }
        public virtual DbSet<TblCity> TblCity { get; set; }
        public virtual DbSet<TblCompany> TblCompany { get; set; }
        public virtual DbSet<TblCountry> TblCountry { get; set; }
        public virtual DbSet<TblDepartment> TblDepartment { get; set; }
        public virtual DbSet<TblDocumentCategory> TblDocumentCategory { get; set; }
        public virtual DbSet<TblEmployee> TblEmployee { get; set; }
        public virtual DbSet<TblEmployeeDocument> TblEmployeeDocument { get; set; }
        public virtual DbSet<TblRegion> TblRegion { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }

        
    }
}
