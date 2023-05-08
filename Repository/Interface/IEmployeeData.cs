using CRUDApi.Models;
using EmployeeManagementAPI.ViewModels.Request;
using EmployeeManagementAPI.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Repository.Interface
{
	public interface IEmployeeData
	{
		Task<IList<EmployeeResponse>> GetAllEmployeesAsync(CancellationToken token);
		Task<TblEmployee> GetEmployeeAsync(int id, CancellationToken token);
		Task AddEmployeeAsync(EmployeeCreateVM employee);
		Task UpdateEmployeeAsync(EmployeeUpdateVM employee);

	}
}
