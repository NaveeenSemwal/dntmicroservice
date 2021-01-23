using CatalogService.Abstract;
using CatalogService.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.Implementation
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly DatabaseContext db;

        public CatalogRepository(DatabaseContext _db)
        {
            db = _db;
        }
        public void AddProduct(Product model)
        {
            db.Products.Add(model);
            db.SaveChanges();
        }

        public IEnumerable<Product> GetProducts()
        {
           return db.Products.ToList();
        }
    }
}
