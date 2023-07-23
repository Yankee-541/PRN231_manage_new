using BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
	public interface ICommentBusiness
	{
		Task<int> CreateCmt(CommenDTO dto);
		Task<CommenDTO> GetCmtByNewId(int newId);

	}
}
