using BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
	public interface IAuthBusiness
	{
		Task<AccountDTo> GetAccountAsync(string? username, string? password, bool isActive);
		Task<AccountDTo> RegisterAsync(RegisterDTO account);
	}
}
