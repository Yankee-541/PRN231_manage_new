using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mapping
{
	public class AutoMapper : Profile
	{

		public AutoMapper()
		{
			CreateMap<News, NewsDTO>();

		}

	}
}
