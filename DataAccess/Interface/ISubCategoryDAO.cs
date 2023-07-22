using BusinessLogic.DTO;

namespace DataAccess.Interface
{
    public interface ISubCategoryDAO
    {
        Task<List<SubCategoryDTO>> GetAllAsync();

        Task<SubCategoryDTO> GetSubCategoryByIDAsync(int id);

        Task CreateSubCategoryAsync(SubCategoryDTO subCategory);

        Task UpdateSubCategoryAsync(SubCategoryDTO subCategory);

        Task DeleteSubCategoryAsync(int id);
    }
}
