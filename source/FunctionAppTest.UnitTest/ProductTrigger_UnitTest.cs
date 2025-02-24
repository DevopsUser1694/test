using Azure.Core;
using FunctionAppTest.DAL;
using FunctionAppTest.Models;
using FunctionAppTest.Triggers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace FunctionAppTest.UnitTest
{
    [TestFixture]
    internal class ProductTrigger_UnitTest
    {
        private AppDbContext _dbContext;
        private ILogger<ProductTrigger> _logger;
        private ProductTrigger _function;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "UserDatabase")                
                .Options;

            _dbContext = new AppDbContext(options);
            _logger = new LoggerFactory().CreateLogger<ProductTrigger>();

            // Seed the database
            _dbContext.Products.Add(new Product { Id = 1, ProductName = "DefaultP1", CreatedBy = "Chandan", CreatedOn = DateTime.Now, ModifiedBy = "Chandan", ModifiedOn = DateTime.Now });
            _dbContext.Products.Add(new Product { Id = 2, ProductName = "DefaultP2", CreatedBy = "Amit", CreatedOn = DateTime.Now, ModifiedBy = "Amit", ModifiedOn = DateTime.Now });

            _dbContext.SaveChanges();

            _function = new ProductTrigger(_dbContext, _logger);
        }

        private HttpRequest CreateMockHttpRequest(string method, string body = null)
        {
            var context = new DefaultHttpContext();
            var request = context.Request;
            request.Method = method;

            if (body != null)
            {
                var stream = new MemoryStream(Encoding.UTF8.GetBytes(body));
                request.Body = stream;
                request.ContentLength = stream.Length;
            }

            return request;
        }

        [Test]
        public async Task GetProduct_ReturnAllProduct()
        {
            var request = CreateMockHttpRequest("GET");
            var result = await _function.GetProduct(request) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual(result.Value, "Product added successfully!");
        }
        [Test]
        public async Task CreateProduct_ShouldReturnSuccessfull()
        {
            // Arrange
            var newProduct = new Product { Id = 3, ProductName = "New Product", CreatedBy = "user", CreatedOn = DateTime.Now, ModifiedBy = "newuser", ModifiedOn = DateTime.Now };
            var request = CreateMockHttpRequest("POST", JsonConvert.SerializeObject(newProduct));

            // Act
            var result = await _function.AddProduct(request) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, "Product added successfully!");
        }

        [Test]
        public async Task UpdateProduct_ShouldUpdateProduct()
        {
            // Arrange
            var updatedProduct = new Product { Id = 1, ProductName = "DefaultP1", CreatedBy = "Chandan", CreatedOn = DateTime.Now, ModifiedBy = "Anuj", ModifiedOn = DateTime.Now };
            var request = CreateMockHttpRequest("PUT", JsonConvert.SerializeObject(updatedProduct));


            // Act
            var result = await _function.UpdateProduct(request) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, "Product Updated successfully!");
        }

        [Test]
        public async Task DeleteProduct_ShouldRemoveProduct()
        {
            // Arrange
            var existingUser = new Product { Id = 1, ProductName = "DefaultP1", CreatedBy = "Chandan", CreatedOn = DateTime.Now, ModifiedBy = "Chandan", ModifiedOn = DateTime.Now };
            var request = CreateMockHttpRequest("DELETE", JsonConvert.SerializeObject(existingUser));

            // Act
            var result = await _function.RemoveProduct(request) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, "Product removed successfully!");
        }

    }
}
