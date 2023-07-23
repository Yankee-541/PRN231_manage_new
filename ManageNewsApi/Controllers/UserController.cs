using BusinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interface;

namespace ManageNewsApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserAsync()
        {
            var users = await _userBusiness.GetAllAsync(true);
            return users != null ? Ok(users) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _userBusiness.GetByIdAsync(id);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public async Task CreateAsync(UserDTO dto)
        {
            await _userBusiness.CreateAsync(dto);
        }

        [HttpPut]
        public async Task UpdateAsync(UserDTO dto)
        {
            await _userBusiness.UpdateAsync(dto);
        }

        [HttpDelete]
        public async Task DeleteAsync(int id)
        {
            await _userBusiness.DeleteAsync(id);
        }
    }
}
