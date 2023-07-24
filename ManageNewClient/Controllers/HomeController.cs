using ManageNewClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ManageNewClient.Controllers
{
	public class HomeController : BaseController
	{
		private const string _url = "";

		[HttpGet]
		public async Task<IActionResult> Index()
		{
            var account = GetSession();
            ViewBag.currentUser = account;
            return View();
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			HttpContext.Session.Clear();
			return Redirect("../Auth/Login");
		}
	}
}