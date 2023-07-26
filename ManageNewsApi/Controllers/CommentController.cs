using BusinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;
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
        public async Task CreateAsync(CommenDTO dto)
        {
            await _cmtBusiness.CreateCmt(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetCmtByNewIdAsync(int newId)
        {
            var cmt = await _cmtBusiness.GetCmtByNewId(newId);
            return cmt != null ? Ok(cmt) : NotFound();
        }
    }
}
