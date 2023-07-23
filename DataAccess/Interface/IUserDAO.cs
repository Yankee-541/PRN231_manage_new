using BusinessLogic.DTO;

namespace DataAccess.Interface
{
    public interface IUserDAO
    {
        Task<UserDTO> GetByIdAsync(int id);

        Task<List<UserDTO>> GetAllAsync(bool isActive);

        Task CreateAsync(UserDTO dto);

        Task UpdateAsync(UserDTO dto);

        Task DeleteAndRestoreAsync(int id, bool active);
    }
}
