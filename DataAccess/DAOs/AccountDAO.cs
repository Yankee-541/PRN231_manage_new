﻿using BusinessLogic.Models;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
	public class AccountDAO : IAccountDAO
	{
		private readonly ApplicationDbContext _dbContext;

		public AccountDAO(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<User> GetAccountAsync(string? username, string? password, bool isActive)
		{
			if(isActive == false)
			{
				var query = _dbContext.Users.FirstOrDefaultAsync(x => x.Username.Equals(username) && x.Password.Equals(password));
				return await query;
			}else if (isActive == true) {
				return await _dbContext.Users.FirstOrDefaultAsync(x => x.Username.Equals(username) && x.Password.Equals(password) && x.IsActive == true);
			}
			return null;
			
		}
	}
}
