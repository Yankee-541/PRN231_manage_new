using BusinessLogic.DTO;
using DataAccess.DAOs;
using DataAccess.Interface;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Business
{
	public class CommentBusiness : ICommentBusiness
	{
		private readonly ICommentDAO _cmtDAO;

		public CommentBusiness(ICommentDAO newsDAO)
		{
			_cmtDAO = newsDAO;
		}
		public async Task<int> CreateCmt(CommenDTO dto)
		{
			return await _cmtDAO.CreateAsync(dto);
		}
	}
}
