using Fall22Demo.Controllers;
using Fall22Demo.Controllers.api;
using Fall22Demo.Data;
using Fall22Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsController = Fall22Demo.Controllers.ProductsController;

namespace F22DemoTests
{
    [TestClass]
    public class UnitTest1
    {
        private ApplicationDbContext _context;
        List<Product> products = new List<Product>();
        ProductsController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            // create Mock data and add to in-memory database
            var category = new Category
            {
                CategoryId = 100,
                Name = "Some Category"
            };
            _context.Categories.Add(category);

            products.Add(new Product
            {
                CategoryId = 10,
                Name = "Prod 1",
                Price = 10,
                Category = category
            });

            products.Add(new Product
            {
                CategoryId = 20,
                Name = "Prod 0",
                Price = 11,
                Category = category
            });

            foreach (var p in products)
            {
                _context.Add(p);
            }
            _context.SaveChanges();
            controller = new ProductsController(_context);

        }

        [TestMethod]
        public void IndexLoadsProducts()
        {
            // act
            var result = controller.Index();
            var viewResult = (ViewResult)result.Result;
            List<Product> model = (List<Product>)viewResult.Model;

            // assert
            CollectionAssert.AreEqual(products.OrderBy(p => p.Name).ToList(), model);
        }


        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}