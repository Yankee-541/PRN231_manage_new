using BusinessLogic.DTO;
using BusinessLogic.Models;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class SubCategoryDAO : ISubCategoryDAO
    {
        private readonly ApplicationDbContext _dbContext;

        public SubCategoryDAO(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SubCategoryDTO>> GetAllAsync()
        {
            return await _dbContext.SubCategories.Select(sc =>
            new SubCategoryDTO
            {
                Id = sc.Id,
                Name = sc.Name,
                CategoryId = sc.CategoryId,
            }).ToListAsync();
        }

        public async Task<SubCategoryDTO> GetSubCategoryByIDAsync(int id)
        {
            return await _dbContext.SubCategories.Select(x => new SubCategoryDTO
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
            }).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateSubCategoryAsync(SubCategoryDTO subCategory)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var newSubCategory = new SubCategory
                {
                    Id = subCategory.Id,
                    Name = subCategory.Name,
                    CategoryId = subCategory.CategoryId,
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }

        public async Task UpdateSubCategoryAsync(SubCategoryDTO subCategory)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var sc = await _dbContext.SubCategories.FirstOrDefaultAsync(x => x.Id == subCategory.Id);
                sc.Name = subCategory.Name;
                _dbContext.Entry(sc).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }

        public async Task DeleteSubCategoryAsync(int id)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var sc = await _dbContext.SubCategories.FirstOrDefaultAsync(x => x.Id == id);

                _dbContext.SubCategories.Remove(sc);
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
