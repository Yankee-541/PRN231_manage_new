using BusinessLogic.DTO;
using BusinessLogic.Models;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class CategoryDAO : ICategoryDAO
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryDAO(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            return await _dbContext.Categories.Select(c =>
            new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
            }).ToListAsync();
        }
    }
}
