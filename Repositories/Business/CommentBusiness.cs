using BusinessLogic.DTO;
using DataAccess.Interface;
using Repositories.Interface;

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

        public async Task<List<CommenDTO>> GetCmtByNewId(int newId)
        {
            return await _cmtDAO.GetCmtByNewId(newId);
        }
    }
}
