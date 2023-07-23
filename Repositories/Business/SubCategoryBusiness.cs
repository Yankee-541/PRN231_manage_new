using BusinessLogic.DTO;
using DataAccess.Interface;

namespace Repositories.Business
{
    public class SubCategoryBusiness : ISubCategoryDAO
    {
        private readonly ISubCategoryDAO _subCategoryDAO;

        public SubCategoryBusiness(ISubCategoryDAO subCategoryDAO)
        {
            _subCategoryDAO = subCategoryDAO;
        }

        public async Task CreateSubCategoryAsync(SubCategoryDTO subCategory)
        {
            await _subCategoryDAO.CreateSubCategoryAsync(subCategory);
        }

        public async Task DeleteSubCategoryAsync(int id)
        {
            await _subCategoryDAO.DeleteSubCategoryAsync(id);
        }

        public async Task<List<SubCategoryDTO>> GetAllAsync()
        {
            return await _subCategoryDAO.GetAllAsync();
        }

        public async Task<SubCategoryDTO> GetSubCategoryByIDAsync(int id)
        {
            return await _subCategoryDAO.GetSubCategoryByIDAsync(id);
        }

        public async Task UpdateSubCategoryAsync(SubCategoryDTO category)
        {
            await _subCategoryDAO.UpdateSubCategoryAsync(category);
        }
    }
}
