using BusinessLogic.DTO;

namespace Repositories.Interface
{
    public interface ICommentBusiness
    {
        Task<int> CreateCmt(CommenDTO dto);

        Task<List<CommenDTO>> GetCmtByNewId(int newId);
    }
}
