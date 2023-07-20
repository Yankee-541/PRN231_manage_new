using BusinessLogic.DTO;
using DataAccess.Interface;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Business
{
	public class AuthBusiness : IAuthBusiness
	{
		private readonly IAccountDAO _accountDao;
		public AuthBusiness(IAccountDAO accountDao)
		{
			_accountDao = accountDao;
		}
		public async Task<AccountDTo> GetAccountAsync(string? username, string? password)
		{
			var account = await _accountDao.GetAccountAsync(username, password);
			if (account == null)
			{
				return null;
			}

			return new AccountDTo()
			{
				Id = account.Id,
				Username = account.Username,
				
			};
		}
	}
}
