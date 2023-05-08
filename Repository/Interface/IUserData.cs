using CRUDApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Repository.Interface
{
	public interface IUserData
	{
		TblUser validateUser(string userName, string Password, string Role);
	}
}
