using BusinessLogic.DTO;
using ManageNewClient.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace ManageNewsClient.Controllers
{
    public class AdminController : BaseController
    {
        private const string _urlGetUser = "https://localhost:7255/api/User/";
        private const int pageSize = 10;



        public async Task<IActionResult> Index(int page = 1)
        {
            var account = GetSession();
            if (account == null)
            {
                return Redirect("../Auth/Login");
            }
            if (account.Role != 1)
            {
                return Redirect("../Home");
            }
            ViewBag.currentUser = account;

            HttpResponseMessage responseMessage = await httpClient.GetAsync(_urlGetUser + $"GetAllUser/true");
            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var response = await responseMessage.Content.ReadFromJsonAsync<List<UserDTO>>();
                    ViewBag.currentPage = page;
                    ViewBag.TotalPage = (int)Math.Ceiling((double)response.Count() / pageSize); 
                    return View(response.Skip((page - 1) * pageSize).Take(pageSize).ToList());

                case System.Net.HttpStatusCode.NotFound:
                    return NotFound();

                case System.Net.HttpStatusCode.Forbidden:
                    return StatusCode(StatusCodes.Status403Forbidden);

                case System.Net.HttpStatusCode.Unauthorized:
                    return Redirect("../Auth/Login");

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
			if (account.Role != 1)
			{
				return Redirect("../Home");
			}
			ViewBag.currentUser = account;

			HttpResponseMessage responseMessage = await httpClient.DeleteAsync(_urlGetUser + $"Delete?id={id}");
			switch (responseMessage.StatusCode)
			{
				case System.Net.HttpStatusCode.OK:
					return Redirect($"../../Admin");
				case System.Net.HttpStatusCode.Conflict:
					return View("Error");
				case System.Net.HttpStatusCode.Forbidden:
					return StatusCode(StatusCodes.Status403Forbidden);
				case System.Net.HttpStatusCode.Unauthorized:
					return Redirect("./Auth/Login");

			}
			return View();
		}
		public async Task<IActionResult> Bin(int page = 1)
		{
			var account = GetSession();
			if (account == null)
			{
				return Redirect("../Auth/Login");
			}
			if (account.Role != 1)
			{
				return Redirect("../Home");
			}
			ViewBag.currentUser = account;

			HttpResponseMessage responseMessage = await httpClient.GetAsync(_urlGetUser + $"GetAllUser/false");
			switch (responseMessage.StatusCode)
			{
				case System.Net.HttpStatusCode.OK:
					var response = await responseMessage.Content.ReadFromJsonAsync<List<UserDTO>>();
					ViewBag.currentPage = page;
					ViewBag.TotalPage = (int)Math.Ceiling((double)response.Count() / pageSize);
					return View(response.Skip((page - 1) * pageSize).Take(pageSize).ToList());

				case System.Net.HttpStatusCode.NotFound:
					return NotFound();

				case System.Net.HttpStatusCode.Forbidden:
					return StatusCode(StatusCodes.Status403Forbidden);

				case System.Net.HttpStatusCode.Unauthorized:
					return Redirect("../Auth/Login");

			}
			return View();
		}


		public async Task<IActionResult> Restore(int id)
		{
			var account = GetSession();
			if (account == null)
			{
				return Redirect("../Auth/Login");
			}
			if (account.Role != 1)
			{
				return Redirect("../Home");
			}
			ViewBag.currentUser = account;

			HttpResponseMessage responseMessage = await httpClient.DeleteAsync(_urlGetUser + $"Restore?id={id}");
			switch (responseMessage.StatusCode)
			{
				case System.Net.HttpStatusCode.OK:
					return Redirect($"../bin");
				case System.Net.HttpStatusCode.Conflict:
					return View("Error");
				case System.Net.HttpStatusCode.Forbidden:
					return StatusCode(StatusCodes.Status403Forbidden);
				case System.Net.HttpStatusCode.Unauthorized:
					return Redirect("./Auth/Login");

			}
			return View();
		}


	}
}
