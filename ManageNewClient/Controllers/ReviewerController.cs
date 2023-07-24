using BusinessLogic.DTO;
using ManageNewClient.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace ManageNewsClient.Controllers
{
	public class ReviewerController : BaseController
	{
		private const string _urlNewQueue = "https://localhost:7255/api/News/";
		private const int pageSize = 10;

		[HttpGet]
		public async Task<IActionResult> News(int page = 1)
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
					return Redirect("../../Auth/Login");

			}

			return View();
		}

        public async Task<IActionResult> Accept(int id)
        {
            var account = GetSession();
            if (account == null)
            {
                return Redirect("../Auth/Login");
            }
            if (account.Role != 4)
            {
                return Redirect("../../Home");
            }
            ViewBag.currentUser = account;

            HttpResponseMessage responseMessage = await httpClient.DeleteAsync(_urlNewQueue + $"EditStatus/{id}");
            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Redirect($"../../Reviewer/News");
                case System.Net.HttpStatusCode.Conflict:
                    return View("Error");
                case System.Net.HttpStatusCode.NotFound:
                    return View("Not Found");
                case System.Net.HttpStatusCode.Forbidden:
                    return StatusCode(StatusCodes.Status403Forbidden);
                case System.Net.HttpStatusCode.Unauthorized:
                    return Redirect("../../Auth/Login");

            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var account = GetSession();
            if (account == null)
            {
                return Redirect("../Auth/Login");
            }
            if (account.Role != 4)
            {
                return Redirect("../../Home");
            }
            ViewBag.currentUser = account;

            HttpResponseMessage responseMessage = await httpClient.DeleteAsync(_urlNewQueue + $"Delete?id={id}");
            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Redirect($"../../Reviewer/News");
                case System.Net.HttpStatusCode.Conflict:
                    return View("Error");
                case System.Net.HttpStatusCode.Forbidden:
                    return StatusCode(StatusCodes.Status403Forbidden);
                case System.Net.HttpStatusCode.Unauthorized:
                    return Redirect("./Auth/Login");

            }
            return View();
        }


        public async Task<IActionResult> DetailsNew(int id)
		{

			return View();
		}

	}
}
