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
		public async Task<AccountDTo> GetAccountAsync(string? username, string? password, bool isActive)
		{
			var account = await _accountDao.GetAccountAsync(username, password, isActive);
			if (account == null)
			{
				return null;
			}

			return new AccountDTo()
			{
				Id = account.Id,
				Username = account.Username,
				Name = account.Name,
				Role = account.Role,


			};
		}

        public async Task<AccountDTo> RegisterAsync(RegisterDTO accounts)
        {
            var account = await _accountDao.RegisterAsync(accounts);
            if (account == null)
            {
                return null;
            }

            return new AccountDTo()
            {
                Id = account.Id,
                Username = account.Username,
                Name = account.Name,
                Role = account.Role,
            };
        }
    }
}
