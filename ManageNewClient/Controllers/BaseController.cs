using BusinessLogic.DTO;
using DataAccess.DAOs;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.Json;

namespace ManageNewClient.Controllers
{
    public class BaseController : Controller
    {
        protected HttpClient httpClient = null;

        public BaseController()
        {
            httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }
        protected void SetSession(AccountDTo account)
        {
            HttpContext.Session.SetString("account", JsonSerializer.Serialize(account));
        }

        protected AccountDTo GetSession()
        {
            string accountValue = HttpContext.Session.GetString("account");
            if ( accountValue == null)
            {
                return null;
            }

            var account = JsonSerializer.Deserialize<AccountDTo>(accountValue);
            ViewBag.account = account.Username;
            return account;
        }

    }
}
