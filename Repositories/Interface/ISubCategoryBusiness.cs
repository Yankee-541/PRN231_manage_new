using BusinessLogic.DTO;

namespace Repositories.Interface
{
    public interface ISubCategoryBusiness
    {
        Task<List<CategoryDTO>> GetAllAsync();

        Task<CategoryDTO> GetSubCategoryByIDAsync(int id);

        Task CreateSubCategoryAsync(CategoryDTO category);

        Task UpdateSubCategoryAsync(CategoryDTO category);

        Task DeleteSubCategoryAsync(int id);
    }
}
