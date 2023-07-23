using BusinessLogic.DTO;

namespace Repositories.Interface
{
    public interface ICategoriesBusiness
    {
        Task<List<CategoryDTO>> GetAllAsync();

        Task<CategoryDTO> GetCategoryByID(int id);

        Task CreateCategory(CategoryDTO category);

        Task UpdateCategory(CategoryDTO category);

        Task DeleteCategoryByID(int id);
    }
}
