using Microsoft.AspNetCore.Mvc;
using IqueiriumBackendProject.Src.Application.Dtos.Products;
using IqueiriumBackendProject.Src.Application.Services.Products;

namespace IqueiriumBackendProject.Src.Api.Controllers.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductFeedbackController : ControllerBase
    {
        private readonly ProductFeedbackService _feedbackService;

        /// <summary>
        /// Inicializa uma nova instância do controlador <see cref="ProductFeedbackController"/>.
        /// </summary>
        /// <param name="feedbackService">
        /// Serviço responsável pela manipulação de feedbacks de produtos.
        /// </param>
        public ProductFeedbackController(ProductFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        /// <summary>
        /// Endpoint para submeter um novo feedback de produto.
        /// </summary>
        /// <param name="feedbackDto">
        /// Objeto contendo os dados necessários para criar um novo feedback, incluindo o ID do produto, 
        /// o conteúdo do feedback, o tipo de feedback e o ID do usuário que o submeteu.
        /// </param>
        /// <returns>
        /// Uma resposta HTTP 201 (Created) com o feedback criado e um link para recuperar o feedback por ID.
        /// </returns>
        // Endpoint para enviar feedback sobre um produto
        [HttpPost("submit")]
        public async Task<ActionResult<ProductFeedbackResponseDTO>> SubmitFeedback([FromBody] ProductFeedbackCreateDTO feedbackDto)
        {
            var createdFeedback = await _feedbackService.SubmitFeedback(feedbackDto);
            return CreatedAtAction(nameof(GetFeedbackById), new { id = createdFeedback.Id }, createdFeedback);
        }

        /// <summary>
        /// Endpoint para obter todos os feedbacks associados a um produto específico.
        /// </summary>
        /// <param name="productId">
        /// O ID do produto cujos feedbacks devem ser recuperados.
        /// </param>
        /// <returns>
        /// Uma resposta HTTP 200 (OK) contendo uma coleção de objetos <see cref="ProductFeedbackResponseDTO"/> 
        /// representando os feedbacks do produto especificado.
        /// </returns>
        // Endpoint para listar feedbacks de um produto específico
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<ProductFeedbackResponseDTO>>> GetFeedbacksByProduct(int productId)
        {
            var feedbacks = await _feedbackService.GetFeedbacksByProduct(productId);
            return Ok(feedbacks);
        }

        /// <summary>
        /// Endpoint para obter um feedback específico com base no ID fornecido.
        /// </summary>
        /// <param name="id">
        /// O ID do feedback que deve ser recuperado.
        /// </param>
        /// <returns>
        /// Uma resposta HTTP 200 (OK) contendo o objeto <see cref="ProductFeedbackResponseDTO"/> 
        /// representando o feedback solicitado, ou uma resposta HTTP 404 (NotFound) se o feedback não for encontrado.
        /// </returns>
        // Endpoint para obter um feedback específico
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductFeedbackResponseDTO>> GetFeedbackById(int id)
        {
            var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return Ok(feedback);
        }
    }
}
