using BusinessLogic.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Business;
using Repositories.Interface;

namespace ManageNewsApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesBusiness _categoriesBusiness;
        public CategoriesController(ICategoriesBusiness categoriesBusiness)
        {
            _categoriesBusiness = categoriesBusiness;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync() 
        {
            var cats = await _categoriesBusiness.GetAllAsync();
            return cats != null ? Ok(cats) : NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var user = await _categoriesBusiness.GetCategoryByID(id);
            return user != null ? Ok(user) : NotFound();
        }
        [HttpPost]
        public async Task CreateCategory(CategoryDTO dto)
        {
            await _categoriesBusiness.CreateCategory(dto);
        }
        [HttpPut]
        public async Task UpdateCategory(CategoryDTO dto)
        {
            await _categoriesBusiness.UpdateCategory(dto);
        }
        [HttpDelete]
        public async Task DeleteCategory(int id)
        {
            await _categoriesBusiness.DeleteCategoryByID(id);
        }
    }
}
