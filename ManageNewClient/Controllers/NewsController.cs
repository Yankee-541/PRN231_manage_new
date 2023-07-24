using BusinessLogic.DTO;
using ManageNewClient.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ManageNewsClient.Controllers
{
    public class NewsController : BaseController
	{

		public IActionResult Index()
        {
            return View();
        }

		public async Task<IActionResult> Details(int id)
        {
            return View();
        }
    }
}
