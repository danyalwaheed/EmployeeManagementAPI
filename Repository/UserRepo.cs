using CRUDApi.Models;
using EmployeeManagementAPI.Repository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Repository
{
	public class UserRepo : IUserData
	{
		private EmployeeApiDBContext _db;
		private readonly IConfiguration _config;

		public UserRepo(EmployeeApiDBContext db, IConfiguration config)
		{
			_db = db;
			_config = config;
		}
		public TblUser validateUser(string userName, string Password, string Role)
		{
			var result = _db.TblUser
				.Where(u => u.Username.Equals(userName))
				.Where(u => u.Password == Password)
				.Where(u => u.Role.Equals(Role))
				.FirstOrDefault();
			return result;
		}
	}
}
