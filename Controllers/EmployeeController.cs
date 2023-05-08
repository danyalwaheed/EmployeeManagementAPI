using CRUDApi.Models;
using EmployeeManagementAPI.Repository;
using EmployeeManagementAPI.Repository.Interface;
using EmployeeManagementAPI.Utilities;
using EmployeeManagementAPI.ViewModels.Request;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Controllers
{
	[EnableCors("CorsPolicy")]
	[Route("[controller]/[action]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IEmployeeData _employeeRepo;
		private readonly IValidationData _validationRepo;
		public EmployeeController(IEmployeeData employeeRepo, IValidationData validationRepo)
		{
			_employeeRepo = employeeRepo;
			_validationRepo = validationRepo;
		}
		/// <summary>
		/// Get All Employees
		/// </summary>
		/// <response code="200">Returns Employees data successfully.</response>
		/// <response code="404">Record not found.</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<TblEmployee>> GetAllEmployees(CancellationToken token)
		{
			return Ok(await _employeeRepo.GetAllEmployeesAsync(token));
		}
		/// <summary>
		/// Get Employee by id
		/// </summary>
		/// <response code="200">Returns Employee data successfully.</response>
		/// <response code="404">Record not found.</response>
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetEmployee([FromRoute] int id, CancellationToken token)
		{
			return Ok(await _employeeRepo.GetEmployeeAsync(id, token));
		}

		/// <summary>
		/// Create Employee
		/// </summary>
		/// <param name="model"></param>
		/// <response code="204">Employee Created Successfully</response>
		/// <response code="400">Invalid request.</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateEmployee(EmployeeCreateVM model, CancellationToken token = default)
		{
			await ValidateDepartment(model.DepartmentId, token);
			await ValidateBranch(model.BranchId, token);
			await ValidateCountry(model.NationalityId, token);

			await _employeeRepo.AddEmployeeAsync(model);
			return Ok("Save Successfully");
		}

		/// <summary>
		/// Update Employee
		/// </summary>
		/// <param name="model"></param>
		/// <response code="200">Employee Updated Successfully</response>
		/// <response code="400">Invalid Request.</response>
		/// <response code="404">Record not found.</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> UpdateEmployee(EmployeeUpdateVM model, CancellationToken token = default)
		{
			await ValidateDepartment(model.DepartmentId, token);
			await ValidateBranch(model.BranchId, token);
			await ValidateCountry(model.NationalityId, token);

			await _employeeRepo.UpdateEmployeeAsync(model);
			return Ok("Updated Successfully");
		}
		private async Task ValidateDepartment(int departmentId, CancellationToken token = default)
		{
			if (!await _validationRepo.isValidDepartmentIdAsync(departmentId, token))
			{
				throw new ApiException(
					ErrorCodesEnum.ERROR_INVALID_DEPARTMENT_ID,
					StatusCodes.Status400BadRequest);
			}
		}
		private async Task ValidateCountry(int countryid, CancellationToken token = default)
		{
			if (!await _validationRepo.isValidCountryIdAsync(countryid, token))
			{
				throw new ApiException(
					ErrorCodesEnum.ERROR_INVALID_COUNTRY_ID,
					StatusCodes.Status400BadRequest);
			}
		}
		private async Task ValidateBranch(int branchid, CancellationToken token = default)
		{
			if (!await _validationRepo.isValidBranchIdAsync(branchid, token))
			{
				throw new ApiException(
					ErrorCodesEnum.ERROR_INVALID_BRANCH_ID,
					StatusCodes.Status400BadRequest);
			}
		}

	}
}
