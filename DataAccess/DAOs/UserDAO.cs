using BusinessLogic.DTO;
using BusinessLogic.Models;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class UserDAO : IUserDAO
    {
        private readonly ApplicationDbContext _dbContext;

        public UserDAO(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(UserDTO dto)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = new User
                {
                    Username = dto.Username,
                    Password = dto.Password,
                    Name = dto.Name,
                    Role = dto.Role,
                    Dob = dto.Dob
                };
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            return await _dbContext.Users.Select(u =>
            new UserDTO
            {
                Id = u.Id,
                Name = u.Name,
                Username = u.Username,
                Dob = u.Dob,
                Role = u.Role
            }).ToListAsync();
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            return await _dbContext.Users.Select(u => new UserDTO
            {
                Id = u.Id,
                Name = u.Name,
                Username = u.Username,
                Dob = u.Dob,
                Role = u.Role
            }).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(UserDTO dto)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == dto.Id);
                user.Name = dto.Name;
                user.Dob = dto.Dob;
                user.Role = dto.Role;

                _dbContext.Entry(user).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }
    }
}
