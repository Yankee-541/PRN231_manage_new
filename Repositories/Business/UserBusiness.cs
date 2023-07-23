using BusinessLogic.DTO;
using DataAccess.Interface;
using Repositories.Interface;

namespace Repositories.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserDAO _userDAO;

        public UserBusiness(IUserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        public async Task CreateAsync(UserDTO dto)
        {
            await _userDAO.CreateAsync(dto);
        }

        public async Task DeleteAsync(int id)
        {
            await _userDAO.DeleteAsync(id);
        }

        public async Task<List<UserDTO>> GetAllAsync(bool isActive)
        {
            return await _userDAO.GetAllAsync(isActive);
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            return await _userDAO.GetByIdAsync(id);
        }

        public async Task UpdateAsync(UserDTO dto)
        {
            await _userDAO.UpdateAsync(dto);
        }
    }
}
