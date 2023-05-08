using EmployeeManagementAPI.Repository.Interface;
using EmployeeManagementAPI.Utilities;
using EmployeeManagementAPI.ViewModels.Request;
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

	[EnableCors("CorsPolicy")]
	[Route("[controller]/[action]")]
	[ApiController]
	public class DocumentController : ControllerBase
	{
		private readonly IEmployeeDocumentData _documentRepo;
		private readonly IValidationData _validationRepo;

		public DocumentController(IEmployeeDocumentData documentRepo, IValidationData validationRepo)
		{
			_documentRepo = documentRepo;
			_validationRepo = validationRepo;
		}
		/// <summary>
		/// Upload Employee Document
		/// </summary>
		/// <param name="model">Employee Document</param>
		
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UploadDocument([FromForm] EmployeeDocument model)
		{
			await ValidateEmployee(model.EmployeeId);
			await ValidateDocumentCategory(model.DocumentCategoryId);
			await _documentRepo.AddDocumentAsync(model);
			return Ok("Document Saved Successfully");
		}

		/// <summary>
		/// Get Employee Document
		/// </summary>
		/// <param name="employeeDocumentId"></param>
		/// <response code="200">Employee document</response>
		/// <response code="404">Employee document Id is invalid or document could not be found</response>
		[HttpGet("{employeeDocumentId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<FileStreamResult> DownloadDocument(int employeeDocumentId)
		{
			return await _documentRepo.GetEmployeeDocumentById(employeeDocumentId);
		}

		/// <summary>
		/// List Employee Documents
		/// </summary>
		/// <param name="employeeId">Employee Id to fetch documents</param>
		/// <response code="200">collection of employee document records</response>
		/// <response code="400">Invalid employee Id</response>
		[HttpGet("{employeeId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<EmployeeDocumentResponseVM>> GetEmployeeDocuments(int employeeId)
		{
			await ValidateEmployee(employeeId);
			var result = await _documentRepo.GetEmployeeDocumentsAsync(employeeId);
			return Ok(result);
		}

		private async Task ValidateEmployee(int employeeid)
		{
			if (!await _validationRepo.isValidEmployeeIdAsync(employeeid))
			{
				throw new ApiException(
					ErrorCodesEnum.ERROR_INVALID_EMPLOYEE_ID,
					StatusCodes.Status400BadRequest);
			}
		}

		private async Task ValidateDocumentCategory(int documentid)
		{
			if (!await _validationRepo.isValidDocumentCategoryIdAsync(documentid))
			{
				throw new ApiException(
					ErrorCodesEnum.ERROR_INVALID_DOCUMENT_CATEGORY_ID,
					StatusCodes.Status400BadRequest);
			}
		}


	}
}
