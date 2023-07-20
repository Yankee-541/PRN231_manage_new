using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
	public class AccountDTo
	{
		public int Id { get; set; }

		public string Username { get; set; } = null!;

		public string Password { get; set; } = null!;

		public int Role { get; set; }

		public string Name { get; set; } = null!;
	}
}
