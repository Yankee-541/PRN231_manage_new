﻿using BusinessLogic.DTO;
using BusinessLogic.Models;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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
				var cmt = new Comment {
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
	}
}