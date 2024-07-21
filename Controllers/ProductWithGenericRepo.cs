using Microsoft.AspNetCore.Mvc;
using RepositoryPatternCrudEFCore.Entity;
using RepositoryPatternCrudEFCore.Repository;
using RepositoryPatternCrudEFCore.ViewModel;

namespace RepositoryPatternCrudEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWithGenericRepoController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;

        public ProductWithGenericRepoController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var products = await _productRepository.GetByIdAsync(id);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductRequest product)
        {
            var productEntity = new Product()
            {
                ProductName = product.ProductName,
                Price = product.Price,
            };
            var createdProductResponse = await _productRepository.AddAsync(productEntity);

            return CreatedAtAction(nameof(GetById), new { id = createdProductResponse.ProductId }, createdProductResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductRequest product)
        {
            var productEntity = await _productRepository.GetByIdAsync(id);
            if (productEntity == null)
            {
                return NotFound();
            }
            productEntity.ProductName = product.ProductName;
            productEntity.Price = product.Price;
            await _productRepository.UpdateAsync(productEntity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async  Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();

            }
            await _productRepository.DeleteAsync(product);
            return NoContent();

        }
    }
}