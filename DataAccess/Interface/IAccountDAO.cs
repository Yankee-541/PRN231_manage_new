using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
	public interface IAccountDAO
	{
		Task<User> GetAccountAsync(string? username, string? password, bool isActive);
	}
}
