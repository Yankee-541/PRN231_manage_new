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
			//ViewBag.LeftMenu = true;
			var account = GetSession();
            //ViewBag.account = account.Name;
            
            //if (account == null)
            //{
            //	return Redirect("../Auth/Login");
            //}
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