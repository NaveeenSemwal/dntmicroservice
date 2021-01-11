using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShoppingWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWebUI.Controllers
{
    public class AccountController : Controller
    {
        HttpClient client;
        Uri uri;
        IConfiguration _config;
        public AccountController(IConfiguration config)
        {
            _config = config;
            uri = new Uri(_config["ApiAddress"]);

            client = new HttpClient();
            client.BaseAddress = uri;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            string strData = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(strData, Encoding.UTF8, "application/json");

            var response = client.PostAsync(client.BaseAddress + "authentication", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                UserViewModel user = JsonConvert.DeserializeObject<UserViewModel>(data);

                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddMinutes(5);
                HttpContext.Response.Cookies.Append("token", user.Token, options);

                if (user != null)
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "user" });
                }
            }
            return View();
        }
    }
}
