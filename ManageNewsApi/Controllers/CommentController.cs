using BusinessLogic.DTO;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Business;
using Repositories.Interface;

namespace ManageNewsApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly ICommentBusiness _cmtBusiness;


		public CommentController(ICommentBusiness newsBusiness)
		{
			_cmtBusiness = newsBusiness;
		}

		[HttpPost]
		public async Task CreateAsync([FromBody] CommenDTO dto)
		{
			var userId = Int32.Parse(User.FindFirst("Id")?.Value);
			dto.CreatedBy = userId;
			await _cmtBusiness.CreateCmt(dto);
			
		}

		[HttpGet]
		public async Task<IActionResult> GetCmtByNewIdAsync(int newId)
		{
			var users = await _cmtBusiness.GetCmtByNewId(newId);
			return users != null ? Ok(users) : NotFound();
		}

	}
}
