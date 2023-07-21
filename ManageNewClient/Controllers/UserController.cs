using Microsoft.AspNetCore.Mvc;

namespace ManageNewsClient.Controllers
{
    public class UserController : Controller
    {
        private const string _urlUser = "https://localhost:7255/api/User/";
        public IActionResult Index()
        {
            return View();
        }
    }
}
