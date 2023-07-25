using BusinessLogic.DTO;

namespace Repositories.Interface
{
    public interface INewsBusiness
    {
        Task<List<NewsDTO>> SearchAsync(SearchModel searchModel);

        Task<List<NewsDTO>> GetListNews(int status,string? search, int categoryId);

        Task<NewsDTO> GetByIdAsync(int id);

        Task CreateAsync(NewsDTO dto);

        Task EditAsync(NewsDTO dto);
        Task DeleteAndRestoreAsync(int id, bool active);

    }
}
