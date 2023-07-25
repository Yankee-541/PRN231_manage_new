using BusinessLogic.DTO;

namespace DataAccess.Interface
{
    public interface ICommentDAO
    {
        Task<int> CreateAsync(CommenDTO dto);

        Task<List<CommenDTO>> GetCmtByNewId(int id);
    }
}
