﻿using BusinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interface;

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
        public async Task<List<NewsDTO>> GetListNews()
        {
            return await _newsBusiness.GetListNews();
        }

        [HttpGet]
        [Route("id")]
        public async Task<NewsDTO> GetByIdAsync(int id)
        {
            return await _newsBusiness.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task CreateAsync([FromForm] NewsDTO dto, [FromForm] List<IFormFile> images)
        {
            dto.Id = await _newsBusiness.CreateAsync(dto);
            dto.ImgPath = await SaveImage(dto.Id, images);
            await _newsBusiness.EditAsync(dto);
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
