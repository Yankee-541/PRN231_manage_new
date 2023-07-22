using BusinessLogic.DTO;

namespace Repositories.Interface
{
    public interface INewsBusiness
    {
        Task<List<NewsDTO>> SearchAsync(SearchModel searchModel);

        Task<List<NewsDTO>> GetListNews();

        Task<NewsDTO> GetByIdAsync(int id);

        Task<int> CreateAsync(NewsDTO dto);

        Task EditAsync(NewsDTO dto);
    }
}
