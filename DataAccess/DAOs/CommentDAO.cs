using BusinessLogic.DTO;
using BusinessLogic.Models;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class CommentDAO : ICommentDAO
    {
        private readonly ApplicationDbContext _dbContext;

        public CommentDAO(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(CommenDTO dto)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var cmt = new Comment
                {
                    CreatedBy = dto.CreatedBy,
                    Content = dto.Content,
                    NewsId = dto.NewsId,
                };
                cmt.IsActive = true;
                cmt.CreateDate = DateTime.UtcNow.AddHours(7);

                var entity = await _dbContext.Comments.AddAsync(cmt);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return entity.Entity.Id;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<CommenDTO>> GetCmtByNewId(int id)
        {
            return await _dbContext.Comments.Include(x => x.CreatedByNavigation).Select(u => new CommenDTO
            {
                Name = u.CreatedByNavigation.Name,
                Content = u.Content,
                CreateDate = u.CreateDate,
                NewsId = u.NewsId,
                CreatedBy = u.CreatedBy
            }).Where(x => x.NewsId == id).ToListAsync();
        }
    }
}
