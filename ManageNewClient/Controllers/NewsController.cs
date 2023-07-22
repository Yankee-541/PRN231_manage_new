using Microsoft.AspNetCore.Mvc;

namespace ManageNewsClient.Controllers
{
    public class NewsController : Controller
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
