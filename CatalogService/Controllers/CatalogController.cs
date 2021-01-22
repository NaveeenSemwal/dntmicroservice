using CatalogService.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly DatabaseContext db;
        public CatalogController(DatabaseContext _db)
        {
            db = _db;
        }

        public IEnumerable<Product> GetProducts()
        {
            return db.Products.ToList();
        }

        [HttpPost]
        public IActionResult AddProduct(Product model)
        {
            try
            {
                db.Products.Add(model);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
