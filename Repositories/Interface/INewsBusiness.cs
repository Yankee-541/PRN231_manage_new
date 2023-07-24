using BusinessLogic.DTO;

namespace Repositories.Interface
{
    public interface INewsBusiness
    {
        Task<List<NewsDTO>> SearchAsync(SearchModel searchModel);

        Task<List<NewsDTO>> GetListNews(int status);

        Task<NewsDTO> GetByIdAsync(int id);

        Task<int> CreateAsync(NewsDTO dto);

        Task EditAsync(NewsDTO dto);
        Task DeleteAndRestoreAsync(int id, bool active);

    }
}
