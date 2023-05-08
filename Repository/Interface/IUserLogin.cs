using CRUDApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Repository.Interface
{
	public interface IUserLogin
	{
		Task<UserLogin> GetUserByUsername(string username);
	}
}
