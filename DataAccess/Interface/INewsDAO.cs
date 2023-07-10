using BusinessLogic.DTO;

namespace DataAccess.Interface
{
    public interface INewsDAO
    {
        Task<List<NewsDTO>> SearchAsync(SearchModel searchModel);

        Task<NewsDTO> GetByIdAsync(int id);

        Task<int> CreateAsync(NewsDTO dto);

        Task EditAsync(NewsDTO dto);
    }
}
