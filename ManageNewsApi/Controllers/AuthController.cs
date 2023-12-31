﻿using BusinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.Interface;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiProject.Constants;

namespace ManageNewsApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthBusiness _authBusiness;
		private readonly IConfiguration _configuration;

		public AuthController(IAuthBusiness authBusiness, IConfiguration configuration)
		{
			_authBusiness = authBusiness;
			_configuration = configuration;
		}

		[HttpPost]
		public async Task<IActionResult> LoginAsync([FromBody] LoginModelDTO account)
		{
			var response = await _authBusiness.GetAccountAsync(account.Username, account.Password, false);
			if (response == null)
			{
				return NotFound();
			}
			var response2 = await _authBusiness.GetAccountAsync(account.Username, account.Password, true);
			if (response2 == null)
			{
				return BadRequest();
			}

			var token = GenerateToken(response2);

			var loginResponse = new LoginModelResponse()
			{
				userDTO = response2,
				Token = token
			};

			return Ok(loginResponse);
		}

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO account)
        {
			var register = await _authBusiness.RegisterAsync(account);


            //var response2 = await _authBusiness.GetAccountAsync(register.Username, register.Password, true);
            //if (response2 == null)
            //{
            //    return BadRequest();
            //}

            var token = GenerateToken(register);

            var loginResponse = new LoginModelResponse()
            {
                userDTO = register,
                Token = token
            };

            return Ok(loginResponse);
        }

        private string GenerateToken(AccountDTo account)
		{
			var role = Roles.User;

			if (account.Role == 1)
			{
				role = Roles.Admin;
			}else if(account.Role == 3)
			{
				role = Roles.Writer;
			}
			else if (account.Role == 4)
			{
				role = Roles.Reviewer;
			}

			#region Tạo token
			try
			{
				//create claims details based on the user information
				var claims = new[] {
							new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
							new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
							new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
							new Claim(ClaimTypes.Role, role),
							new Claim("Id", account.Id.ToString()),
							new Claim("Name", account.Name),
							new Claim("UserName", account.Username)
						};

				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
				var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
				var token = new JwtSecurityToken(null, null, claims,
					expires: DateTime.UtcNow.AddMinutes(30),
					signingCredentials: signIn);

				return new JwtSecurityTokenHandler().WriteToken(token);
			}
			catch (Exception ex)
			{
				return null;
				throw new Exception(ex.Message);
			}

			#endregion
		}

	}
}
