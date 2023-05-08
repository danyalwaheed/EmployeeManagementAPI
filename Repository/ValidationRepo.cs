using CRUDApi.Models;
using EmployeeManagementAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Repository
{
	public class ValidationRepo : IValidationData
	{
		public readonly EmployeeApiDBContext _db;

		public ValidationRepo(EmployeeApiDBContext db)
		{
			_db = db;
		}
		public async Task<bool> isValidBranchIdAsync(int id, CancellationToken token = default)
		{
			return await _db.TblBranch
			  .AsNoTracking()
			  .Where(q => q.BranchId == id)
			  .AnyAsync(token);
		}

		public async Task<bool> isValidCountryIdAsync(int id, CancellationToken token = default)
		{
			return await _db.TblCountry
				 .AsNoTracking()
				 .Where(q => q.CountryId == id)
				 .AnyAsync(token);
		}

		public async Task<bool> isValidDepartmentIdAsync(int id, CancellationToken token = default)
		{
			return await _db.TblDepartment
			   .AsNoTracking()
			   .Where(q => q.DepartmentId == id)
			   .AnyAsync(token);
		}

		public async Task<bool> isValidDocumentCategoryIdAsync(int id, CancellationToken token = default)
		{
			return await _db.TblDocumentCategory
						   .AsNoTracking()
						   .Where(q => q.DocumentCategoryId == id)
						   .AnyAsync(token);
		}

		public async Task<bool> isValidEmployeeIdAsync(int id, CancellationToken token = default)
		{
			return await _db.TblEmployee
						   .AsNoTracking()
						   .Where(q => q.EmployeeId == id)
						   .AnyAsync(token);
		}
		public async Task<bool> isValidUserAsync(string username, string password, CancellationToken token = default)
		{
			return await _db.TblUser
							.AsNoTracking()
							.Where(q => q.Username == username)
							.Where(q => q.Password == password)
							.AnyAsync(token);
		}


	}
}
