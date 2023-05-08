using CRUDApi.Models;
using EmployeeManagementAPI.Repository.Interface;
using EmployeeManagementAPI.Utilities;
using EmployeeManagementAPI.Utilities.File_Upload;
using EmployeeManagementAPI.ViewModels.Request;
using EmployeeManagementAPI.ViewModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Repository
{
	public class DocumentRepo : IEmployeeDocumentData
	{
		private readonly EmployeeApiDBContext _db;
		private readonly string _uploadsDirectory;
		private readonly FileUploadConfig _uploadConfig;
		private readonly IConfiguration _configuration;
		public DocumentRepo(EmployeeApiDBContext db, IOptionsSnapshot<FileUploadConfig> configAccessor,
			IConfiguration configuration)
		{
			_db = db;
			_uploadConfig = configAccessor.Get(FileUploadConfigTypeEnum.EmployeeDocument.ToString());
			_configuration = configuration;
			_uploadsDirectory = configuration.GetValue<string>("UploadsDirectory");
		}

		public async Task AddDocumentAsync(EmployeeDocument model)
		{
			// To verfiy the file
			if (model.Document != null)
			{
				await FileUploadHelper.VerifyUploadedFilesAsync(_uploadConfig, new[] { model.Document });
			}

			var employeDocument = new TblEmployeeDocument
			{
				DepartmentId = model.DepartmentId,
				DocumentCategoryId = model.DocumentCategoryId,
				EmployeeId = model.EmployeeId,
				DocumentPath = string.Empty,
				Remarks = model.Remarks
			};
			if (model.Document != null)
			{
				string uploadsDirectory = _configuration.GetValue<string>("UploadsDirectory");
				employeDocument.DocumentPath = Path.GetFileName(model.Document.FileName);
				string filePath = Path.Combine(uploadsDirectory, employeDocument.DocumentPath);
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await model.Document.CopyToAsync(stream);
				}

			}
			_db.TblEmployeeDocument.Add(employeDocument);
			await _db.SaveChangesAsync();

		}

		public async Task<IList<TblDocumentCategory>> GetAllDocumentCategoriesAsync()
		{
			var a = _db.TblDocumentCategory;
			return await a.ToListAsync();
		}

		//public async Task<FileDownloadResult> GetDocumentAsync(int documentId)
		//{
		//	var record = await FetchDocumentRecord(documentId);
		//	var documentPath = FileUploadHelper.GetUploadDirectory(_uploadConfig, _configuration);
		//	return await FileUploadHelper.GetSavedFileAsync(documentPath, record.DocumentPath);
		//}

		public async Task<FileStreamResult> GetEmployeeDocumentById(int id)
		{
			var record = await FetchDocumentRecord(id);
			var filePath = Path.Combine(@"C:\Uploads", record.DocumentPath);

			// read the file bytes 
			var fileBytes = File.ReadAllBytes(filePath);

			// create a file stream result
			var fileStreamResult = new FileStreamResult(new MemoryStream(fileBytes), "application/octet-stream");
			fileStreamResult.FileDownloadName = record.DocumentPath;

			return fileStreamResult;

		}

		public async Task<IList<EmployeeDocumentResponseVM>> GetEmployeeDocumentsAsync(int employeeId)
		{
			var query = _db.TblEmployeeDocument
				.AsNoTracking()
				.Where(d => d.EmployeeId == employeeId)
				.Join(
					_db.TblDocumentCategory,
					doc => doc.DocumentCategoryId,
					type => type.DocumentCategoryId,
					(doc, type) => new
					{
						doc,
						documentTypeName = type.DocumentName
					}
				).Join(
					_db.TblDepartment,
					data => data.doc.DepartmentId,
					dept => dept.DepartmentId,
					(data, dept) => new
					{
						data.doc,
						data.documentTypeName,
						departmentName = dept.DepartmentName
					}
				).Select(result => new EmployeeDocumentResponseVM
				{
					EmployeeDocumentID = result.doc.EmployeeDocumentId,
					DepartmentName = result.departmentName,
					DocumentTypeName = result.documentTypeName,
					DocumentName = result.doc.DocumentPath,
					Remarks = result.doc.Remarks
				});
			return await query.ToListAsync();
		}

		public async Task<string> GetFileNameAsync(int employeeDocumentId)
		{
			var employeeDocument = await _db.TblEmployeeDocument.FindAsync(employeeDocumentId);

			if (employeeDocument == null)
			{
				return null;
			}

			return employeeDocument.DocumentPath;
		}

		private async Task<TblEmployeeDocument> FetchDocumentRecord(int id, CancellationToken token = default)
		{
			var record = await _db.TblEmployeeDocument
				.AsNoTracking()
				.Where(n => n.EmployeeDocumentId == id)
				.FirstOrDefaultAsync(token);
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
