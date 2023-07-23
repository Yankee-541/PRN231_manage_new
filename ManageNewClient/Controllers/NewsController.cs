using BusinessLogic.DTO;
using ManageNewClient.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ManageNewsClient.Controllers
{
    public class NewsController : BaseController
	{
		private const string _urlNewQueue = "https://localhost:7255/api/News/";
		private const int pageSize = 10;

		[HttpGet]
		public async Task<IActionResult> Queue(int page = 1)
		{
			var account = GetSession();
			if (account == null)
			{
				return Redirect("../Auth/Login");
			}
			if (account.Role != 4)
			{
				return Redirect("../Home");
			}
			ViewBag.currentUser = account;
			HttpResponseMessage responseMessage = await httpClient.GetAsync(_urlNewQueue + $"NewsQueue");
			switch (responseMessage.StatusCode)
			{
				case System.Net.HttpStatusCode.OK:
					var response = await responseMessage.Content.ReadFromJsonAsync<List<NewsDTO>>();
					ViewBag.currentPage = page;
					ViewBag.TotalPage = (int)Math.Ceiling((double)response.Count() / pageSize);
					return View(response.Skip((page - 1) * pageSize).Take(pageSize).ToList());
				case System.Net.HttpStatusCode.Unauthorized:
					return Redirect("../Auth/Login");

			}

			return View();
		}

		public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
			var ind = id;
            return View();
        }
    }
}
