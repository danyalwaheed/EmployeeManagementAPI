using CRUDApi.Models;
using EmployeeManagementAPI.Repository.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Controllers
{

	[EnableCors("CorsPolicy")]
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly IUserLogin _UserLoginRepo;
		public LoginController(IUserLogin UserLoginRepo)
		{
			_UserLoginRepo = UserLoginRepo;
		}
		[HttpPost("authenticate")]
		public async Task<IActionResult> Login([FromBody] UserLogin request)
		{
			var user = await _UserLoginRepo.GetUserByUsername(request.Username);

			if (user == null || !VerifyPassword(request.Password, user.Password))
			{
				return Unauthorized();
			}

			return Ok(request);
		}

		private bool VerifyPassword(string password, string storedPassword)
		{
			return password == storedPassword;
		}


	}
}
