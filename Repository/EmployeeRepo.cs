using CRUDApi.Models;
using EmployeeManagementAPI.Repository.Interface;
using EmployeeManagementAPI.Utilities;
using EmployeeManagementAPI.ViewModels.Request;
using EmployeeManagementAPI.ViewModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Repository
{
	public class EmployeeRepo : IEmployeeData
	{
		private readonly EmployeeApiDBContext _db;
		public EmployeeRepo(EmployeeApiDBContext db)
		{
			_db = db;
		}
		public async Task AddEmployeeAsync(EmployeeCreateVM model)
		{
			var employee = new TblEmployee
			{
				EmployeeName = model.EmployeeName,
				EmployeeEmail = model.EmployeeEmail,
				GrossSalary = model.GrossSalary,
				DepartmentId = model.DepartmentId,
				NationalityId = model.NationalityId,
				BranchId = model.BranchId,
				IsActive = true
			};
			_db.TblEmployee.Add(employee);
			await _db.SaveChangesAsync();
		}

		public async Task<IList<EmployeeResponse>> GetAllEmployeesAsync(CancellationToken token)
		{
			var queryMaster = _db.TblCompany
					   .GroupJoin(
						   _db.TblBranch,
						   comp => comp.CompanyId,
						   branch => branch.CompanyId,
						   (comp, branch) => new { comp, branch }
					   )
					   .SelectMany(
						   obj => obj.branch.DefaultIfEmpty(),
						   (data, branch) => new
						   {
							   data.comp.CompanyName,
							   data.comp.CompanyAddress,
							   branch.BranchId,
							   branch.BranchName,
							   branch.CountryId
						   });

			var query = queryMaster
				.GroupJoin(
					_db.TblEmployee,
					data => data.BranchId,
					emp => emp.BranchId,
					(data, emp) => new { data, emp }
				)
				.SelectMany(
					obj => obj.emp.DefaultIfEmpty(),
					(data, emp) => new
					{
						data.data.CountryId,
						data.data.CompanyName,
						data.data.BranchName,
						data.data.CompanyAddress,
						emp.EmployeeId,
						emp.EmployeeName,
						emp.DepartmentId,
						emp.EmployeeEmail,
						emp.GrossSalary,
						emp.NationalityId,
						emp.IsActive
					}
				 )
				.Join(
					_db.TblCountry,
					dataN => dataN.NationalityId,
					country => country.CountryId,
					(dataN, country) => new { dataN, country }
				)
				.Join(_db.TblCountry,
					data => data.dataN.CountryId,
					country => country.CountryId,
					(data, country) => new { data, country }
				)
				.Select(r => new
				{
					CompaniesName = r.data.dataN.CompanyName,
					CompaniesAddress = r.data.dataN.CompanyAddress,
					BranchesName = r.data.dataN.BranchName,
					branchcountry = r.country.CountryName,
					EmployeeID = r.data.dataN.EmployeeId,
					EmployeesName = r.data.dataN.EmployeeName,
					DepartmentID = r.data.dataN.DepartmentId,
					GrossSalary=r.data.dataN.GrossSalary,
					EmployeesEmail = r.data.dataN.EmployeeEmail,
					Nationality = r.data.country.CountryName,
					ISActive = r.data.dataN.IsActive,
				});
			var result = query
					.Join(
						_db.TblDepartment,
						data => data.DepartmentID,
						dept => dept.DepartmentId,
						(data, dept) => new { data, dept }
					)
					.Select(r => new EmployeeResponse
					{
						CompanyName = r.data.CompaniesName,
						CompanyAddress = r.data.CompaniesAddress,
						BranchName = r.data.BranchesName,
						CountryName = r.data.branchcountry,
						EmployeeID = r.data.EmployeeID,
						EmployeeName = r.data.EmployeesName,
						DepartmentName = r.dept.DepartmentName,
						EmployeeEmail = r.data.EmployeesEmail,
						GrossSalary=r.data.GrossSalary,
						DepartmentId = r.dept.DepartmentId,
						Nationality = r.data.Nationality,
						Status = r.data.ISActive == true ? "Active" : "inActive"
					});
			return await result.ToListAsync(token);
		}

		public async Task<TblEmployee> GetEmployeeAsync(int id, CancellationToken token)
		{
			var result = await ValidateRecord(id);
			return (result);
		}

		public async Task UpdateEmployeeAsync(EmployeeUpdateVM model)
		{
			var record = await ValidateRecord(model.EmployeeId);

			record.EmployeeName = model.EmployeeName;
			record.EmployeeEmail = model.EmployeeEmail;
			record.GrossSalary = model.GrossSalary;
			record.DepartmentId = model.DepartmentId;
			record.BranchId = model.BranchId;
			record.NationalityId = model.NationalityId;


			_db.Update(record);
			await _db.SaveChangesAsync();
		}
		private async Task<TblEmployee> ValidateRecord(int employeeId)
		{
			var record = await _db.TblEmployee
				.AsNoTracking()
				.Where(x => x.EmployeeId == employeeId)
				.FirstOrDefaultAsync();
			if (record == null)
			{
				throw new ApiException(
					ErrorCodesEnum.ERROR_RECORD_NOT_FOUND,
					StatusCodes.Status404NotFound);
			}
			return record;
		}


	}
}
