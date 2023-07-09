﻿using BusinessLogic.DTO;
using DataAccess.Interface;
using Repositories.Interface;

namespace Repositories.Business
{
    public class NewsBusiness : INewsBusiness
    {
        private readonly INewsDAO _newsDAO;

        public NewsBusiness(INewsDAO newsDAO)
        {
            _newsDAO = newsDAO;
        }

        public async Task<List<NewsDTO>> SearchAsync(SearchModel searchModel)
        {
            return await _newsDAO.SearchAsync(searchModel);
        }

        public async Task<NewsDTO> GetByIdAsync(int id)
        {
            return await _newsDAO.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(NewsDTO dto)
        {
            return await _newsDAO.CreateAsync(dto);
        }

        public async Task EditAsync(NewsDTO dto)
        {
            await _newsDAO.EditAsync(dto);
        }
    }
}
