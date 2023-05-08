using CRUDApi.Models;
using EmployeeManagementAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Repository
{
	public class DepartmentRepo : IDepartmentData
	{
		private readonly EmployeeApiDBContext _dbcontext;
		public DepartmentRepo(EmployeeApiDBContext dbcontext)
		{
			_dbcontext = dbcontext;
		}

		public async Task<IList<TblBranch>> GetAllBranchesAsync()
		{
			var branches = _dbcontext.TblBranch;
			return await branches.ToListAsync();
		}

		public async Task<IList<TblCompany>> GetAllCompaniesAsync()
		{
			var companies = _dbcontext.TblCompany;
			return await companies.ToListAsync();
		}

		public async Task<IList<TblCountry>> GetAllCountriesAsync()
		{
			var countries = _dbcontext.TblCountry;
			return await countries.ToListAsync();
		}

		public async Task<IList<TblDepartment>> GetAllDepartmentsAsync()
		{
			var departments = _dbcontext.TblDepartment;
			return await departments.ToListAsync();
		}
	}
}
