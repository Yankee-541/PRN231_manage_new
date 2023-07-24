using BusinessLogic.DTO;
using ManageNewClient.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace ManageNewsClient.Controllers
{
	public class AuthController : BaseController
	{
		private const string _url = "https://localhost:7255/api/Auth/";

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> LoginAsync(LoginModelDTO model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(_url + "Login", model);

			switch (responseMessage.StatusCode)
			{
				case System.Net.HttpStatusCode.OK:
					var response = await responseMessage.Content.ReadFromJsonAsync<LoginModelResponse>();
					KeepToken(response.Token);
					SetSession(response.userDTO);
					if(response.userDTO.Role == 1)
					{
                        return Redirect("../Admin");
                    }
					return Redirect("../Home");
				case System.Net.HttpStatusCode.NotFound:
					ViewData["msg"] = "Username or password is in valid. Please try again!";
					return View(model);
				case System.Net.HttpStatusCode.BadRequest:
					ViewData["msg"] = "Your account may have been locked !!!";
					return View(model);
			}

			return View(model);
		}

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(_url + "Register", model);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var response = await responseMessage.Content.ReadFromJsonAsync<LoginModelResponse>();
                    KeepToken(response.Token);
                    SetSession(response.userDTO);
                    return Redirect("../Home");
                case System.Net.HttpStatusCode.NotFound:
                    ViewData["msg"] = "Username or password is in valid. Please try again!";
                    return View(model);
                case System.Net.HttpStatusCode.BadRequest:
                    ViewData["msg"] = "Your account may have been locked !!!";
                    return View(model);
            }

            return View(model);
        }

        private void KeepToken(string token)
		{
			HttpContext.Session.SetString("AccessToken", JsonSerializer.Serialize(token));
		}

	}
}
