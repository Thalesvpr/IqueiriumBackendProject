using IqueiriumBackendProject.Src.Application.Dtos;
using IqueiriumBackendProject.Src.Application.Services;
using IqueiriumBackendProject.Src.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IqueiriumBackendProject.Src.Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // GET: api/product
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET api/product/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        // POST api/product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProduct = await _productService.AddProductAsync(productDto);
            return CreatedAtAction("POST api/product", new { id = createdProduct.Id }, createdProduct);
        }

        // PUT api/product/id
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] string value)
        {
            return NoContent();
        }

        // DELETE api/product/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}
