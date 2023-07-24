using BusinessLogic.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Business;
using Repositories.Interface;
using WebApiProject.Constants;

namespace ManageNewsApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsBusiness _newsBusiness;

        private const string FOLDER_NAME = "Upload";

        public NewsController(
            INewsBusiness newsBusiness)
        {
            _newsBusiness = newsBusiness;
        }

        [HttpGet]
        public async Task<List<NewsDTO>> SearchAsync(SearchModel searchModel)
        {
            return await _newsBusiness.SearchAsync(searchModel);
        }

        [HttpGet]
        public async Task<List<NewsDTO>> GetListNews(string? search)
        {
            return await _newsBusiness.GetListNews(1, search);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<NewsDTO> GetByIdAsync(int id)
        {
            return await _newsBusiness.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task CreateAsync(NewsDTO dto)
        {
            await _newsBusiness.CreateAsync(dto);
        }

        [HttpGet]
        [Route("{id}/{reviewId}")]
        public async Task<IActionResult> EditStatusAsync(int id, int reviewId)
        {
            var news = await _newsBusiness.GetByIdAsync(id);
            news.Status = 1;
            news.PostedDate = DateTime.Now;
            news.ModifiedBy = reviewId;
            await _newsBusiness.EditAsync(news);
            return Ok();
        }

        [HttpDelete]
        public async Task DeleteAsync(int id)
        {
            await _newsBusiness.DeleteAndRestoreAsync(id, false);
        }


        private async Task<string> SaveImage(int id, List<IFormFile> images)
        {
            string pathFile = Path.Combine(FOLDER_NAME, id.ToString());
            if (!Directory.Exists(pathFile))
            {
                Directory.CreateDirectory(pathFile);
            }
            try
            {
                images.ForEach(async image =>
                {
                    using (var uploading = new FileStream(Path.Combine(pathFile, image.FileName), FileMode.CreateNew))
                    {
                        await image.CopyToAsync(uploading);
                        uploading.Close();
                    }
                });
            }
            catch (Exception)
            {
                throw;
            }

            return pathFile;
        }
    }
}
