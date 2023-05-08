using CRUDApi.Models;
using EmployeeManagementAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Repository
{
	public class UserLoginRepo : IUserLogin
	{
		private readonly EmployeeApiDBContext _context;

		public UserLoginRepo(EmployeeApiDBContext context)
		{
			_context = context;
		}

		public async Task<UserLogin> GetUserByUsername(string username)
		{
			return await _context.Set<UserLogin>().FirstOrDefaultAsync(u => u.Username == username);
		}

	}
}
