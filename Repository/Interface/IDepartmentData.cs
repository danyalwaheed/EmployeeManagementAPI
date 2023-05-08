using CRUDApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Repository.Interface
{
	public interface IDepartmentData
	{
		Task<IList<TblDepartment>> GetAllDepartmentsAsync();
		Task<IList<TblCountry>> GetAllCountriesAsync();
		Task<IList<TblCompany>> GetAllCompaniesAsync();
		Task<IList<TblBranch>> GetAllBranchesAsync();
	}
}
