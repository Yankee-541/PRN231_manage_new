using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Models;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class NewsDAO : INewsDAO
    {
        private readonly ApplicationDbContext _dbContext;
		public NewsDAO(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
		}

		public async Task<List<NewsDTO>> GetListNews(int status)
		{
            var news =  _dbContext.News.Where(n=>n.Status==status).Select(n => new NewsDTO
			{
				Id = n.Id,
				Title = n.Title,
				CategoryId = n.CategoryId,
				Content = n.Content,
				CreatedBy = n.CreatedBy,
				ImgPath = n.ImgPath,
				NumberOfLikes = n.NumberOfLikes,
				PostedDate = n.PostedDate,
				Status = n.Status,
				SubCategoryId = n.SubCategoryId
			}).ToListAsync();
			return await news;
		}


		public async Task<List<NewsDTO>> SearchAsync(SearchModel searchModel)
        {
            var query = _dbContext.News.Select(n => new NewsDTO
            {
                Id = n.Id,
                Title = n.Title,
                CategoryId = n.CategoryId,
                Content = n.Content,
                CreatedBy = n.CreatedBy,
                ImgPath = n.ImgPath,
                NumberOfLikes = n.NumberOfLikes,
                PostedDate = n.PostedDate,
                Status = n.Status,
                SubCategoryId = n.SubCategoryId
            });

            if (!string.IsNullOrEmpty(searchModel.Title))
            {
                query = query.Where(n => n.Title.Contains(searchModel.Title));
            }

            if (searchModel.CategoryId != 0)
            {
                query = query.Where(n => n.CategoryId == searchModel.CategoryId);
            }

            if (searchModel.SubCategoryId != 0)
            {
                query = query.Where(n => n.SubCategoryId == searchModel.SubCategoryId);
            }

            if (searchModel.CreatedBy != 0)
            {
                query = query.Where(n => n.CreatedBy == searchModel.CreatedBy);
            }

            if (searchModel.PostedDate != null)
            {
                query = query.Where(n => n.PostedDate == searchModel.PostedDate);
            }

            return await query.ToListAsync();
        }

        public async Task<NewsDTO> GetByIdAsync(int id)
        {
            return await _dbContext.News.Select(n =>
            new NewsDTO
            {
                Id = n.Id,
                Title = n.Title,
                CategoryId = n.CategoryId,
                Content = n.Content,
                CreatedBy = n.CreatedBy,
                ImgPath = n.ImgPath,
                NumberOfLikes = n.NumberOfLikes,
                PostedDate = n.PostedDate,
                Status = n.Status,
                SubCategoryId = n.SubCategoryId
            }).FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<int> CreateAsync(NewsDTO dto)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var news = new News
                {
                    Title = dto.Title,
                    CategoryId = dto.CategoryId,
                    Content = dto.Content,
                    CreatedDate = DateTime.UtcNow.AddHours(7).Date,
                    CreatedBy = dto.CreatedBy,
                    SubCategoryId = dto.SubCategoryId,
                    Status = 0,
                };
                var entity = await _dbContext.News.AddAsync(news);
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

        public async Task EditAsync(NewsDTO dto)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var news = await _dbContext.News.FirstOrDefaultAsync(n => n.Id == dto.Id);
                news.Title = dto.Title;
                news.Content = dto.Content;
                news.ModifiedBy = dto.ModifiedBy;
                news.Status = dto.Status;
                news.NumberOfLikes = dto.NumberOfLikes;
                news.ImgPath = dto.ImgPath ?? news.ImgPath;
                _dbContext.News.Update(news);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
