using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Models;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccess.DAOs
{
    public class NewsDAO : INewsDAO
    {
        private readonly ApplicationDbContext _dbContext;
		public NewsDAO(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
		}

		public async Task<List<NewsDTO>> GetListNews(int status, string? search, int categoryId)
		{

            List<News> news;

            if (!string.IsNullOrEmpty(search) && categoryId == 0)
            {
                news = await _dbContext.News.Where(n => n.Status == status && n.IsActive == true && n.Title.Contains(search)).ToListAsync();
            }else if(string.IsNullOrEmpty(search) && categoryId != 0)
            {
                news = await _dbContext.News.Where(n => n.Status == status && n.IsActive == true && n.CategoryId == categoryId).ToListAsync();
            }else if (!string.IsNullOrEmpty(search) && categoryId != 0)
            {
                news = await _dbContext.News.Where(n => n.Status == status && n.IsActive == true && n.CategoryId == categoryId && n.Title.Contains(search)).ToListAsync();
            }
            else
            {
               news = await _dbContext.News.Where(n => n.Status == status && n.IsActive == true).ToListAsync();
            }

            NewsDTO newsDTO;
            List< NewsDTO > newsDTOList = new List<NewsDTO>();
            foreach (var item in news)
            {
                User author = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == item.CreatedBy);

                newsDTO = new NewsDTO
                {
                    Id = item.Id,
                    Title = item.Title,
                    CategoryId = item.CategoryId,
                    Content = item.Content,
                    CreatedBy = item.CreatedBy,
                    ImgPath = item.ImgPath,
                    NumberOfLikes = item.NumberOfLikes,
                    PostedDate = item.PostedDate,
                    Status = item.Status,
                    SubCategoryId = item.SubCategoryId,
                    Created = author.Name,
                    
                };
                newsDTOList.Add(newsDTO);
            }

            
            

            return newsDTOList;
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
            return await _dbContext.News.Include(x => x.CreatedByNavigation).Select(n =>
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
                SubCategoryId = n.SubCategoryId,
                Created = n.CreatedByNavigation.Name
            }).FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task CreateAsync(NewsDTO dto)
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
                    ImgPath = dto.ImgPath,
                    IsActive = true
                };
                await _dbContext.News.AddAsync(news);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
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

        public async Task DeleteAndRestoreAsync(int id, bool active)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var news = await _dbContext.News.FirstOrDefaultAsync(x => x.Id == id);
                news.IsActive = active;
                _dbContext.Entry(news).State = EntityState.Modified;
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
