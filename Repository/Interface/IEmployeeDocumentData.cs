using CRUDApi.Models;
using EmployeeManagementAPI.ViewModels.Request;
using EmployeeManagementAPI.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Repository.Interface
{
	public interface IEmployeeDocumentData
	{
		Task AddDocumentAsync(EmployeeDocument model);
		Task<IList<TblDocumentCategory>> GetAllDocumentCategoriesAsync();
		//Task<FileDownloadResult> GetDocumentAsync(int documentId);
		Task<FileStreamResult> GetEmployeeDocumentById(int id);
		Task<IList<EmpDocResponseVM>> GetEmployeeDocumentsAsync(int employeeId);

	}
}
