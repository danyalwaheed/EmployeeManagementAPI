using EmployeeManagementAPI.Repository.Interface;
using EmployeeManagementAPI.ViewModels.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Controllers
{
	/// <summary>
	/// This section contains endpoints for DEPARTMENT .
	/// </summary>
	[EnableCors("CorsPolicy")]
	[ApiController]
	[Route("[controller]/[action]")]
	public class GeneralController : ControllerBase
	{
		private readonly IDepartmentData _departmentRepo;
		private readonly IEmployeeDocumentData _documentRepo;
		public GeneralController(IDepartmentData departmentRepo, IEmployeeDocumentData docrepo)
		{
			_departmentRepo = departmentRepo;
			_documentRepo = docrepo;
		}
		/// <summary>
		/// Get All Departments
		/// </summary>
		/// <response code="200">Returns All Departments successfully.</response>
		/// <response code="404">Records not found.</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<DepartmentResponse>> AllDepartments()
		{
			var dep = await _departmentRepo.GetAllDepartmentsAsync();
			return Ok(dep);
		}
		/// <summary>
		/// Get All Countries
		/// </summary>
		/// <response code="200">Returns All Countries successfully.</response>
		/// <response code="404">Records not found.</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<CountryResponse>> AllCountries()
		{
			var countries = await _departmentRepo.GetAllCountriesAsync();
			return Ok(countries);
		}
		/// <summary>
		/// Get All Branches
		/// </summary>
		/// <response code="200">Returns All Branches successfully.</response>
		/// <response code="404">Records not found.</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<BranchResponse>> AllBranches()
		{
			var branches = await _departmentRepo.GetAllBranchesAsync();
			return Ok(branches);
		}
		/// <summary>
		/// Get All Companies
		/// </summary>
		/// <response code="200">Returns All Companies successfully.</response>
		/// <response code="404">Records not found.</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<CompanyResponse>> AllCompanies()
		{
			var companies = await _departmentRepo.GetAllCompaniesAsync();
			return Ok(companies);
		}
		/// <summary>
		/// Get All Documents Categories
		/// </summary>
		/// <response code="200">Returns All DocumentCategories data successfully.</response>
		/// <response code="404">Records not found.</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<DocumentcategoryResponse>> GetAllDocumentCategories()
		{
			var result = await _documentRepo.GetAllDocumentCategoriesAsync();
			return Ok(result);
		}


	}
}
