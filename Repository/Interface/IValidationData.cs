using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Repository.Interface
{
	public interface IValidationData
	{
		Task<bool> isValidDepartmentIdAsync(int id, CancellationToken token = default);
		Task<bool> isValidUserAsync(string username, string password, CancellationToken token = default);
		Task<bool> isValidBranchIdAsync(int id, CancellationToken token = default);
		Task<bool> isValidCountryIdAsync(int id, CancellationToken token = default);
		Task<bool> isValidDocumentCategoryIdAsync(int id, CancellationToken token = default);
		Task<bool> isValidEmployeeIdAsync(int id, CancellationToken token = default);
	}
}
