using BusinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interface;

namespace ManageNewsApi.Controllers
{
	[Route("api/[controller]")]
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
		[Route("Login")]
		public async Task<IActionResult> LoginAsync([FromBody] LoginModelDto account)
		{
			var response = await _authBusiness.GetAccountAsync(account.Username, account.Password);
			if (response == null)
			{
				return NotFound();
			}

			//var token = GenerateToken(response);

			var loginResponse = new LoginModelResponse()
			{
				userDTO = response,
				//Token = token
			};

			return Ok(loginResponse);
		}


	}
}
