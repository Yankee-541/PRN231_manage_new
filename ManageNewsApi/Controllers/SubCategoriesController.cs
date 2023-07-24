using Microsoft.AspNetCore.Mvc;
using Repositories.Interface;

namespace ManageNewsApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ISubCategoryBusiness _subCategoriesBusiness;

        public SubCategoriesController(ISubCategoryBusiness subCategoriesBusiness)
        {
            _subCategoriesBusiness = subCategoriesBusiness;
        }

        [HttpGet]
        [Route("{categoryId}")]
        public async Task<IActionResult> GetAllAsync(int categoryId)
        {
            var cats = await _subCategoriesBusiness.GetAllAsync(categoryId);
            return cats != null ? Ok(cats) : NotFound();
        }
    }
}
