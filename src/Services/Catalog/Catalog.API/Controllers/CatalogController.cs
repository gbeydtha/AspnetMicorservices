using Catalog.API.Entites;
using Catalog.API.Entites.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController: ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;  
        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public  ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = GetPreconfigurationProducts();  //_repository.GetProducts();
            return Ok(products); 
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType( typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _repository.GetProduct(id); 
            if(product == null)
            {
                _logger.LogError($"Product with id: {id}, not found");
                return NotFound(); 
            }
            return Ok(product); 
        }

        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var products = await _repository.GetProductByCategory(category);
            return Ok(products); 
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _repository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product); 
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return  Ok(await _repository.UpdateProduct(product));
            
        }

        [HttpDelete("{id:length(24)}", Name ="DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            return Ok(await _repository.DeleteProduct(id));

        }

        private  IEnumerable<Product> GetPreconfigurationProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name= "IPhone X",
                    Summery = "This phone is the company's biggest change to its flagship smartphone in years.",
                    Description= "This phone is the company's biggest change to its flagship smartphone in years.",
                    Category= "Smart Phone",
                    ImageFile= "product-1.png",
                    Price= 950.00M
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name= "IPhone 12",
                    Summery = "This phone is the company's biggest change to its flagship smartphone in years.",
                    Description= "This phone is the company's biggest change to its flagship smartphone in years.",
                    Category= "Smart Phone",
                    ImageFile= "product-2.png",
                    Price= 1200.00M
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Name= "IPhone 12x",
                    Summery = "This phone is the company's biggest change to its flagship smartphone in years.",
                    Description= "This phone is the company's biggest change to its flagship smartphone in years.",
                    Category= "Smart Phone",
                    ImageFile= "product-3.png",
                    Price= 1500.00M
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f8",
                    Name= "Samsung Note 12",
                    Summery = "This phone is the company's biggest change to its flagship smartphone in years.",
                    Description= "This phone is the company's biggest change to its flagship smartphone in years.",
                    Category= "Smart Phone",
                    ImageFile= "product-4.png",
                    Price= 1500.00M
                }
            };
        }
    }
}
