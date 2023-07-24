using BusinessLogic.DTO;

namespace Repositories.Interface
{
    public interface ISubCategoryBusiness
    {
        Task<List<SubCategoryDTO>> GetAllAsync(int categoryId);

        Task<SubCategoryDTO> GetSubCategoryByIDAsync(int id);

        Task CreateSubCategoryAsync(SubCategoryDTO category);

        Task UpdateSubCategoryAsync(SubCategoryDTO category);

        Task DeleteSubCategoryAsync(int id);
    }
}
