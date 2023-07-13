using BusinessLogic.DTO;
using BusinessLogic.Models;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class CategoriesDAO : ICategoriesDAO
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoriesDAO(ApplicationDbContext dbContext)
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
        public async Task<CategoryDTO> GetCategoryByID(int id)
        {
            return await _dbContext.Categories.Select(cat => new CategoryDTO { Id = cat.Id,
                Name = cat.Name, }).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateCategory(CategoryDTO category)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var NewCategory = new Category
                {
                    Id= category.Id,
                    Name = category.Name,
                };
                await _dbContext.Categories.AddAsync(NewCategory);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }
        public async Task UpdateCategory(CategoryDTO category)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var cat = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
                cat.Name = category.Name;
                _dbContext.Entry(cat).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }
        public async Task DeleteCategoryByID(int id)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var cat = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

                _dbContext.Categories.Remove(cat);
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
