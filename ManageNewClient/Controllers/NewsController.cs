using BusinessLogic.DTO;
using ManageNewClient.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ManageNewsClient.Controllers
{
    public class NewsController : BaseController
    {
        private readonly IWebHostEnvironment _environment;
        private const string FOLDER_NAME = "Upload";
        private const string _url = "https://localhost:7255/api/News/";

        public NewsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        public IActionResult Create()
        {
            var account = GetSession();
            ViewBag.currentUser = account;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(IFormFile file, NewsDTO dto)
        {
            var account = GetSession();
            string uploadFolder = Path.Combine(_environment.WebRootPath, FOLDER_NAME);
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string fileExtention = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString() + fileExtention;
            string pathFile = Path.Combine(_environment.WebRootPath, FOLDER_NAME, fileName);

            dto.CreatedBy = account.Id;
            dto.ImgPath = Path.Combine(FOLDER_NAME, fileName);
            dto.NumberOfLikes = 0;
            dto.Status = 0;

            using (var uploading = new FileStream(pathFile, FileMode.Create))
            {
                await file.CopyToAsync(uploading);
                uploading.Close();

                HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(_url + "Create", dto);
                switch (responseMessage.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        return View();

                    case System.Net.HttpStatusCode.Conflict:
                        if (System.IO.File.Exists(pathFile))
                        {
                            System.IO.File.Delete(pathFile);
                        }
                        return View("Error");

                    case System.Net.HttpStatusCode.Forbidden:
                        return StatusCode(StatusCodes.Status403Forbidden);

                    case System.Net.HttpStatusCode.Unauthorized:
                        return Redirect("../Auth/Login");
                }
            }

            return View();
        }
    }
}
