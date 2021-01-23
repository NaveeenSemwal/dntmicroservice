using CatalogService.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.Abstract
{
    public interface ICatalogRepository
    {
        IEnumerable<Product> GetProducts();

        void AddProduct(Product model);
    }
}
