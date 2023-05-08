using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Repository.Interface
{
	public interface IJwtAuthData
	{
		string Authentication(string username, string password, string Role);
	}
}
