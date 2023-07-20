using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
	public class LoginModelResponse
	{
		public AccountDTo userDTO { get; set; }
		public string Token { get; set; }
	}
}
