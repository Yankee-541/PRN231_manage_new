using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
	public class CommenDTO
	{
		public string Username { get; set; }

		public string Content { get; set; } = null!;

		public DateTime CreateDate { get; set; }

		public int NewsId { get; set; }

		public int CreatedBy { get; set; }
	}
}
