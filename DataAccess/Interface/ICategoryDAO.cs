using BusinessLogic.DTO;

namespace DataAccess.Interface
{
    public interface ICategoryDAO
    {
        Task<List<CategoryDTO>> GetAllAsync();
    }
}
