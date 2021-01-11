using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShoppingWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShoppingWebUI.Areas.User.Controllers
{
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CatalogController : BaseController
    {
        HttpClient client;
        Uri uri;
        IConfiguration _config;

        public CatalogController(IConfiguration config)
        {
            _config = config;
            uri = new Uri(_config["ApiAddress"]);

            client = new HttpClient();
            client.BaseAddress = uri;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductViewModel> model = new List<ProductViewModel>();

            var token = HttpContext.Request.Cookies["token"];
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = client.GetAsync(client.BaseAddress + "catalog").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<IEnumerable<ProductViewModel>>(data);
            }
            return View(model);
        }
    }
}
