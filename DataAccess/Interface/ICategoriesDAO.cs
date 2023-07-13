using BusinessLogic.DTO;

namespace DataAccess.Interface
{
    public interface ICategoriesDAO
    {
        Task<List<CategoryDTO>> GetAllAsync();

        Task<CategoryDTO> GetCategoryByID(int id);

        Task CreateCategory(CategoryDTO category);

        Task UpdateCategory(CategoryDTO category);

        Task DeleteCategoryByID(int id);
    }
}
