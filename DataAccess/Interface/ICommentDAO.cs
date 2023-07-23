using BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
	public interface ICommentDAO
	{
		Task<int> CreateAsync(CommenDTO dto);
		Task<CommenDTO> GetCmtByNewId(int id);
	}
}
