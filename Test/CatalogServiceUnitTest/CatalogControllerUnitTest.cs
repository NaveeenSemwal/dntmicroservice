using CatalogService.Abstract;
using CatalogService.Controllers;
using CatalogService.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace CatalogServiceUnitTest
{
    /// <summary>
    /// https://stackoverflow.com/questions/42555356/organizing-test-names-in-mstest
    /// https://dzone.com/articles/7-popular-unit-test-naming
    /// </summary>

    [TestClass]
    public class CatalogControllerUnitTest
    {
        List<Product> _products = null;

        public CatalogControllerUnitTest()
        {
            _products = new List<Product>();

            _products.Add(new Product { ProductId = 1, Name = "TV", UnitPrice = 1500, CategoryId = 1 });
        }

        [TestMethod]
        [TestCategory("Catalog Controller")]
        public void GetProducts_WhenRequested_ThenResultShouldNotBeNull()
        {
            // Arrange
            Mock<ICatalogRepository> _db = new Mock<ICatalogRepository>();
            CatalogController catalogController = new CatalogController(_db.Object);

            // Act
            _db.Setup(x => x.GetProducts()).Returns(_products);
            IEnumerable<Product> products = catalogController.GetProducts();

            // Assert
            Assert.AreEqual(products, _products);
        }
    }
}
