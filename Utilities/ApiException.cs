using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Utilities
{
	public class ApiException : Exception
	{
		public int StatusCode { get; } = 500;
		public int ErrorCode { get; }
		public string Details { get; }
		public object[] FormatterArguments { get; } = { };

		public ApiException(string message, int statusCode, int errorCode, string details = null) : base(message)
		{
			StatusCode = statusCode;
			ErrorCode = errorCode;
			Details = details;
		}

		public ApiException(ErrorCodesEnum error, int statusCode, string details = null, object[] arguments = null) :
			base(error.ToString())
		{
			StatusCode = statusCode;
			ErrorCode = (int)error;
			Details = details;
			FormatterArguments = arguments ?? new object[] { };
		}
	}

	public class APIResponseExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			if (context.Exception is ApiException apiException)
			{
				var problem = new ProblemDetails
				{
					Title = "API Exception",
					Status = (int)apiException.StatusCode,
					Detail = apiException.Message,
				};
				problem.Extensions.Add("error-code", apiException.ErrorCode);
				if (!string.IsNullOrEmpty(apiException.Details))
				{
					problem.Extensions.Add("details", apiException.Details);
				}

				context.Result = new ObjectResult(problem)
				{
					StatusCode = (int)apiException.StatusCode,
					DeclaredType = typeof(ProblemDetails),
				};
				context.ExceptionHandled = true;
			}
		}


		public void OnActionExecuting(ActionExecutingContext context)
		{
		}

		public int Order => int.MaxValue; // run last
	}

	public enum ErrorCodesEnum
	{
		ERROR_RECORD_NOT_FOUND = 111,
		ERROR_INVALID_COUNTRY_ID = 300,
		ERROR_INVALID_DEPARTMENT_ID = 301,
		ERROR_INVALID_EMPLOYEE_ID = 302,
		ERROR_INVALID_BRANCH_ID = 303,
		ERROR_INVALID_DOCUMENT_CATEGORY_ID = 305,
		ERROR_FILESIZE_EXCEED = 306,
		ERROR_INVALID_UPLOADED_DOCUMENT = 307,
		ERROR_INVALID_DOCUMENT_FORMAT = 308,
		ERROR_EMPTY_FILE = 309,
		ERROR_INVALID_USERNAME_OR_PASSWORD = 310
	}
}
