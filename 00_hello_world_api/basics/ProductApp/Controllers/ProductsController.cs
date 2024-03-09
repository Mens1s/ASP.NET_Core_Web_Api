using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Models;

namespace ProductApp.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = new List<Product>()
            {
                new Product() {Id = 1, ProductName = "Computer"},
                new Product() {Id = 1, ProductName = "Telephone"},
                new Product() {Id = 1, ProductName = "Jacket"},
                new Product() {Id = 1, ProductName = "F-16"}
            };
            _logger.LogInformation("GetAllProducts action has been called and executed.");
            
            return Ok(products);
        }
        
        [HttpPost]
        public IActionResult GetAllProducts([FromBody]Product product)
        {
            _logger.LogWarning("Product has been created");
            return StatusCode(201);
        }
    }
}
