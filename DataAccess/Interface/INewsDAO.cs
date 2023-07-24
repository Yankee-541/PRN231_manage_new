using BusinessLogic.DTO;

namespace DataAccess.Interface
{
    public interface INewsDAO
    {
        Task<List<NewsDTO>> SearchAsync(SearchModel searchModel);
        Task<List<NewsDTO>> GetListNews(int status, string? search);
		Task<NewsDTO> GetByIdAsync(int id);

        Task CreateAsync(NewsDTO dto);

        Task EditAsync(NewsDTO dto);
        Task DeleteAndRestoreAsync(int id, bool active);

    }
}
