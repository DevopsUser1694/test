using FunctionAppTest.DAL;
using FunctionAppTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FunctionAppTest.Triggers
{
    public class ProductTrigger
    {
        private readonly ILogger<ProductTrigger> _logger;
        private readonly AppDbContext _dbContext;
        public ProductTrigger(AppDbContext dbContext, ILogger<ProductTrigger> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [Function("GetProduct")]
        public async Task<IActionResult> GetProduct([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation($"{nameof(GetProduct)} trigger function processed a request.");
            var products = await _dbContext.Products.ToListAsync();

            return new OkObjectResult(JsonSerializer.Serialize(products));
        }

        [Function("AddProduct")]
        public async Task<IActionResult> AddProduct([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation($"{nameof(AddProduct)} trigger function processed a request.");
            try
            {
                var requestBody = await JsonSerializer.DeserializeAsync<Product>(req.Body);

                if (requestBody == null)
                {
                    return new BadRequestObjectResult("Invalid product data.");
                }

                _dbContext.Products.Add(requestBody);
                await _dbContext.SaveChangesAsync();

                return new OkObjectResult("Product added successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception Occured" + ex.Message + " " + ex.InnerException);
                return new BadRequestObjectResult("Exception Occured.");
            }
        }

        [Function("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequest req)
        {
            _logger.LogInformation($"{nameof(GetProduct)} trigger function processed a request.");
            try
            {
                var product = await JsonSerializer.DeserializeAsync<Product>(req.Body);

                if (product == null)
                {
                    return new BadRequestObjectResult("Invalid product data.");
                }

                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync();

                return new OkObjectResult("Product Updated successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception Occured" + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [Function("RemoveProduct")]
        public async Task<IActionResult> RemoveProduct([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequest req)
        {
            _logger.LogInformation($"{nameof(GetProduct)} trigger function processed a request.");

            try
            {
                var requestBody = await JsonSerializer.DeserializeAsync<Product>(req.Body);

                if (requestBody == null)
                {
                    return new BadRequestObjectResult("Invalid product data.");
                }

                _dbContext.Products.Remove(requestBody);
                await _dbContext.SaveChangesAsync();

                return new OkObjectResult("Product removed successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception Occured" + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
