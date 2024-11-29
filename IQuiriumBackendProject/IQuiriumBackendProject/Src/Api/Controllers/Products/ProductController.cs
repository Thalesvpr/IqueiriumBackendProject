using IqueiriumBackendProject.Src.Application.Dtos.Products;
using IqueiriumBackendProject.Src.Application.Services.Products;
using IqueiriumBackendProject.Src.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IqueiriumBackendProject.Src.Api.Controllers.Products
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        /// <summary>
        /// Inicializa uma nova instância do controlador de produtos.
        /// </summary>
        /// <param name="productService">Serviço de manipulação de produtos.</param>
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Obtém a lista de todos os produtos.
        /// </summary>
        /// <returns>
        /// Uma resposta HTTP 200 com a lista de produtos.
        /// </returns>
        // GET: api/product
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Obtém um produto pelo ID.
        /// </summary>
        /// <param name="id">ID do produto a ser recuperado.</param>
        /// <returns>
        /// Uma resposta HTTP 200 com os dados do produto correspondente ao ID informado.
        /// </returns>
        // GET api/product/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="productDto">Objeto contendo os dados do produto a ser criado.</param>
        /// <returns>
        /// Uma resposta HTTP 201 com o produto criado e um link para o recurso.
        /// </returns>
        // POST api/product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProduct = await _productService.AddProductAsync(productDto);
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <param name="id">ID do produto a ser atualizado.</param>
        /// <param name="value">Valor atualizado do produto.</param>
        /// <returns>
        /// Uma resposta HTTP 204 indicando que a operação foi bem-sucedida.
        /// </returns>
        // PUT api/product/id
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] string value)
        {
            return NoContent();
        }

        /// <summary>
        /// Remove um produto pelo ID.
        /// </summary>
        /// <param name="id">ID do produto a ser removido.</param>
        /// <returns>
        /// Uma resposta HTTP 204 indicando que a operação foi bem-sucedida.
        /// </returns>
        // DELETE api/product/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}
