using BusinessLogic.DTO;
using DataAccess.Interface;
using Repositories.Interface;

namespace Repositories.Business
{
    public class CategoriesBusiness : ICategoriesBusiness
    {
        private readonly ICategoriesDAO _catDAO;
        public CategoriesBusiness(ICategoriesDAO catDAO)
        {
            _catDAO = catDAO;
        }
        public async Task CreateCategory(CategoryDTO category)
        {
            await _catDAO.CreateCategory(category);
        }

        public async Task DeleteCategoryByID(int id)
        {
             await _catDAO.DeleteCategoryByID(id);
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            return await _catDAO.GetAllAsync();
        }

        public async Task<CategoryDTO> GetCategoryByID(int id)
        {
            return await _catDAO.GetCategoryByID(id);
        }

        public async Task UpdateCategory(CategoryDTO category)
        {
            await _catDAO.UpdateCategory(category);
        }
    }

}
